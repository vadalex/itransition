var TodoForm = React.createClass({

    getInitialState: function() {
        return {item: ''};
    },

    handleClick: function(e) {        
        this.props.addClick(this.state.item);
        this.setState({item: ''});        
        return;
    },

    onChange: function(e) {
        this.setState({
            item: e.target.value
        });
    },

    render: function(){
        return (
            <div>
                <input className="add-input" placeholder="I need to..." type="text" onChange={this.onChange} value={this.state.item} />
                <button className="add-btn" onClick={this.handleClick}><h2>Add</h2></button>
            </div>			
		);
}
});