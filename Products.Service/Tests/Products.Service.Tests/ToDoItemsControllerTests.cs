using System;
using System.Linq;
using Products.Data;
using Products.Service.Controllers;
using Xunit;

namespace Products.Service.Tests
{
    public class ToDoItemsControllerTests
    {

        private readonly ToDoItemsController _toDoItemsController = new ToDoItemsController(new ToDoContext());


        [Fact]
        public void Test1()
        {
            var output = _toDoItemsController.GetAllToDoItems();
            Assert.True(output.Result.Any());
        }
    }
}
