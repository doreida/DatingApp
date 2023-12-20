﻿using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IMessageRepository
{
  void AddMessage(Message message);
  void DeleteMessage(Message message);
  Task<Message> GetMessage(int id);
  Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams messageParams);
  Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUserName, string recipientUserName);
  Task<bool> SaveAllAsync();
  void AddGroup(Group group);
  void RemoveConnection(Connection connection);

  Task<Connection> GetConnection(string ConnectionId);

  Task<Group> GetMessageGroup(string groupName);

  Task<Group> GetGroupForConnection(string connectionId);
}
