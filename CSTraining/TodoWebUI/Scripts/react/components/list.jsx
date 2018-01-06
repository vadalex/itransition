var TodoList = React.createClass({

	getPlaceholder: function() {
		var placeholder = document.createElement("li");
        placeholder.className = "placeholder";
		return placeholder;
	},

    getInitialState : function(){
        return { placeholder: this.getPlaceholder() }
    },	
	
    dragStart: function(e) {		
        this.dragged = e.currentTarget;		
        e.dataTransfer.effectAllowed = 'move';
    },    

    dragEnd: function(e) {
        this.dragged.style.display = "block";
        this.dragged.parentNode.removeChild(this.state.placeholder);//this.getPlaceholder());        
        var items = this.props.items;
        var from = Number(this.dragged.dataset.id);
        var to = Number(this.over.dataset.id);				
        if(from < to) to--;
		if(this.nodePlacement == "after") to++;		

        items.splice(to -1 , 0, items.splice(from - 1, 1)[0]);
		$.each(items, function(index, value) {
			value.Order = index + 1;
		});		
        this.props.changeOrder(items);
    },

    dragOver: function(e) {
        e.preventDefault();
        this.dragged.style.display = "none";
        if(e.target.className == "placeholder") return;
		if(e.target.tagName != "LI") return;
        this.over = e.target;		

		var relY = e.clientY - this.over.offsetTop;
		var height = this.over.offsetHeight / 2;
		var parent = e.target.parentNode;
		if(relY > height) {
			this.nodePlacement = "after";
			parent.insertBefore(this.state.placeholder, e.target.nextElementSibling);
		}
		else if(relY < height) {
			this.nodePlacement = "before"
			parent.insertBefore(this.state.placeholder, e.target);
		}
    },
    
    render: function () {
        var self = this;
        var mapItems = function(item){
            return (<TodoItem item={item} 
				remove={self.props.remove} 
				update={self.props.update} 
				dragStart={self.dragStart} 
				dragEnd={self.dragEnd} />)  
        };

        return (<ul onDragOver={self.dragOver}>{self.props.items.map(mapItems)}</ul>);
    }
});