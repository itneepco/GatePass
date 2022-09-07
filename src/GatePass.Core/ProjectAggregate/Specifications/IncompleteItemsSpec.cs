using Ardalis.Specification;

namespace GatePass.Core.ProjectAggregate.Specifications;
public class IncompleteItemsSpec : Specification<ToDoItem>
{
  public IncompleteItemsSpec()
  {
    Query.Where(item => !item.IsDone);
  }
}
