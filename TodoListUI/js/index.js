const API_URL = "https://localhost:5001/api/TodoItems";
const $divTodos = $("#todos");
let todos = [];

// Functions
const getTodos = () => {
  $.ajax({
    url: API_URL,
    type: "GET",
    success: function (data) {
      todos = data;
      $divTodos.html("");
      for (const index in data) {
        const todo = todos[index];
        $divTodos.append(`
          <div class="d-flex align-items-center my-2 border p-1" onchange="updateTodo(${index})">
          <input class="me-2" type="checkbox" id="${todo.id}" ${todo.isDone ? "checked" : ""}>
          <span>${todo.title}</span>
          <button class="btn btn-sm btn-danger ms-auto" onclick="deleteTodo(${todo.id})">
              <i class="fas fa-trash"></i>
          </button>
          </div>
          `);
      }
    }
  });
}

const postTodo = () => {
  const newItem = { id: 0, title: $("#title").val(), isDone: false };

  $.ajax({
    type: "POST",
    url: API_URL,
    contentType: "application/json",
    data: JSON.stringify(newItem),
    success: function (data) {
      $("#title").val("");
      getTodos();
    }
  }
  );
}


const deleteTodo = (id) => {
  Swal.fire({
    title: 'Are you sure?',
    text: "You won't be able to revert this!",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, delete it!'
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        type: "DELETE",
        url: API_URL + "/" + id,
        success: function (data) {
          getTodos();
        }
      });
    }
  })
}

const updateTodo = (index) => {
  var todo = todos[index];
  todo.isDone = !todo.isDone;

  $.ajax({
    url: API_URL + "/" + todo.id,
    type: "PUT",
    contentType: "application/json",
    data: JSON.stringify(todo),
    success: function (data) {
      getTodos();
    }
  }
  );
}

// Events
$("#frmNewTodo").submit((event) => {
  event.preventDefault();
  postTodo();
});

getTodos();