var TodoApp = React.createClass({

    getInitialState: function() {
        return {remains: 0, items: []}
    },   

    componentDidMount: function() {        
        this.updateList();  
    },
    
	updateList: function() {
        var self = this;        

        $.ajax({
            url: '/todoservice/api/values',
            dataType: 'json',
            type: 'get',
            contentType: 'application/json; charset=utf-8',
            async: true,
            processData: false,
            cache: false,
            success: function (data) {                
                self.setState({ 
					items: data.sort(function(a,b){return a.Order - b.Order}), 
					remains: data.filter(function(value) { return !value.IsDone; }).length
				});				
            }
        });
    },

    addClickHandler: function(item) {
        var self = this;
        $.ajax({
            url: '/todoservice/api/values',
            dataType: 'json',
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({TodoText: item, IsDone: false, Order: 1}),
            async: true,
            processData: false,
            cache: false,
            success: function (data) {                
                self.updateList();  
            },
            error: function () { console.log('not ok'); }
        });
    },

    removeClickHandler: function(itemId) {
        var self = this;
        $.ajax({
            url: '/todoservice/api/values/' + itemId,
            dataType: 'json',
            type: 'delete',
            contentType: 'application/json; charset=utf-8',            
            async: true,
            processData: false,
            cache: false,
            success: function (data) {                
                self.updateList();  
            },
            error: function () { console.log('not ok'); }
        });
    },

    updateClickHandler: function(item){
        var self = this;
        $.ajax({
            url: '/todoservice/api/values',
            dataType: 'json',
            type: 'put',
            contentType: 'application/json; charset=utf-8',
            data: item,
            async: true,
            processData: false,
            cache: false,
            success: function () {                
                self.updateList();
                console.log('ok');
            },            
            error: function () {   
                self.updateList();             
                console.log('not ok');
            },            
        });		
    },

	changeOrderHandler: function(items) {
        var self = this;
        self.setState({items: items});		
		$.each(items, function(index, value){
			var item = JSON.stringify({
                TodoItemId: value.TodoItemId, 
                TodoText: value.TodoText, 
                IsDone: value.IsDone, 
                Order: value.Order});
			self.updateClickHandler(item);
		})
    },
		
	render: function () {
        return (
            <div>
                <TodoHeader remains={this.state.remains} />
                <TodoList items={this.state.items} 
					remove={this.removeClickHandler} 
					update={this.updateClickHandler} 
					changeOrder={this.changeOrderHandler} />
                <TodoForm addClick={this.addClickHandler} />
            </div>                
        );
    }
});