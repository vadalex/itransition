var TodoItem = Backbone.Model.extend({
    defaults: {
        TodoText: "todo",
        IsDone: false,
        Order: 1
    },
    idAttribute: "TodoItemId",
    urlRoot: "/todoservice/api/values",
    done: function () {
        this.save({ IsDone: !this.get("IsDone") });
    }
});

var TodoCollection = Backbone.Collection.extend({
    model: TodoItem,
    url: "/todoservice/api/values",
    comparator: function (model) {
        return model.get("TodoItemId");
    },

    remaining: function() {
        return this.where({ IsDone: false });
    }
});


var todoItemView = Backbone.View.extend({
    tagName: "li",

    template: _.template($("#todoItemTemplate").html()),

    events: {
        "click .toggle": "toggleDone",
        "click button.remove-btn": "clear"
    },

    initialize: function() {
        this.listenTo(this.model, 'change', this.render);
        this.listenTo(this.model, 'destroy', this.remove);
    },

    render: function() {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
    },

    toggleDone: function() {
        this.model.done();
    },

    clear: function () {
        this.model.destroy();
    }
});

var todoAppView = Backbone.View.extend({
    el: $('#content'),

    todoHeaderTemplate: _.template($("#todoHeaderTemplate").html()),

    events: {
        'click .add-btn': 'addTodo'
    },
    
    initialize: function () {
        this.input = this.$(".add-input");
        
        this.listenTo(this.model, 'reset', this.addAll);
        this.listenTo(this.model, 'all', this.render);

        this.header = $('#todo-header');
        this.todolist = $('#todo-list');
    },

    render: function() {
        var remaining = this.model.remaining().length;
        if (this.model.length) {
            this.todolist.show();
            this.todolist.empty();
            this.addAll();
            this.header.show();
            this.header.html(this.todoHeaderTemplate({ remaining: remaining }));
        } else {
            this.todolist.hide();
            this.header.hide();
        }
    },

    addOne: function (todo) {
        var view = new todoItemView({ model: todo });
        this.todolist.append(view.render().el);
    },

    addAll: function () {
        this.model.each(this.addOne, this);
    },

    addTodo: function (e) {
        if (!this.input.val()) return;
        this.model.create({ TodoText: this.input.val(), IsDone: false });
        this.input.val('');
    }

});

var todoApp = (function() {
    var api = {
        todos: null,
        appView: null,
        init: function() {
            this.todos = new TodoCollection();
            this.todos.fetch({
                success: function (collection) {
                    this.appView = new todoAppView({ model: collection });
                }
            });
            return this;
        }
    };

    return api;
})();