﻿using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers;
[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public UsersController( IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
       var users=  await _userRepository.GetMembersAsync();
       
       return Ok(users);
        
    }
    
    [HttpGet("{username}")] // api/users/2
    public async Task<ActionResult<MemberDTO>> GetUser(string username)
    {
        return await _userRepository.GetMemberAsync(username);
      
    }

    [HttpPut] 
    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdate)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        if(user==null) return NotFound();
        
        _mapper.Map(memberUpdate, user);
        if(await _userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update user.");
      
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
    {
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        if(user == null) return NotFound();
        var result = await _photoService.AddPhotoAsync(file);
        if(result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url= result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if(user.Photos.Count == 0) photo.IsMain = true;
        user.Photos.Add(photo);

        if(await _userRepository.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetUser),new {username=user.UserName}, _mapper.Map<PhotoDTO>(photo));
        }
        return BadRequest("Problem adding photo");
    }
    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId){
        var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
        if(user == null) return NotFound();
        var photo = user.Photos.FirstOrDefault(x =>x.Id == photoId);
        if(photo==null) return NotFound();
        if(photo.IsMain) return BadRequest("This is already your main photo.");

        var currentMain = user.Photos.FirstOrDefault(x=> x.IsMain);
        if(currentMain!=null) currentMain.IsMain = false;
        photo.IsMain = true;

        if(await _userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Problems with setting the main photo");
    }

}
