var TodoHeader = React.createClass({
    render: function () {
        return (
            <div id="todo-header">
                <h2>You've got <span className="emphasis">{this.props.remains}</span> things to do</h2>
            </div>
        );
    }
});