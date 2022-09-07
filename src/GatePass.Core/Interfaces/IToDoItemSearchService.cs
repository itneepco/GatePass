using Ardalis.Result;
using GatePass.Core.ProjectAggregate;

namespace GatePass.Core.Interfaces;
public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(Guid projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(Guid projectId, string searchString);
}
