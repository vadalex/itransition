var TodoItem = React.createClass({

	getInitialState: function() { 
		return { isDone: this.props.item.IsDone };
	},	
	  
	componentWillReceiveProps: function(nextProps) {
		this.setState({ isDone: nextProps.item.IsDone });		
	},

	onChange: function(e){
		 this.setState({
            isDone: e.target.checked
        });
		var item = JSON.stringify({
                TodoItemId: this.props.item.TodoItemId, 
                TodoText: this.props.item.TodoText, 
                IsDone: e.target.checked, 
                Order: this.props.item.Order});
		this.props.update(item);		
	},  	
	    
    render: function() {
		var self = this;
        return (
			<li data-id={this.props.item.Order} key={this.props.item.TodoItemId} draggable="true" 
				onDragStart={this.props.dragStart} onDragEnd={this.props.dragEnd}>
				<input className="toggle" type="checkbox" checked={this.state.isDone} onChange={this.onChange} />
				<span className={this.state.isDone ? 'done-true': 'done-false'} >{this.props.item.TodoText}</span>
				<button className="remove-btn" onClick={self.props.remove.bind(self, self.props.item.TodoItemId)}>Remove</button>
			</li>); 
    } 
});