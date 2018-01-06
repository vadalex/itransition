require('../Content/site.css');
require('../Content/todoList.css');

var todo = require('./todoController');

var moduleA = require('./myModule');
var m = new moduleA();
m.printStr('str1', 'str6');
console.log(m.name);