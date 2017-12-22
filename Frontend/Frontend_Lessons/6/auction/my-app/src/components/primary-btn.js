import React, { Component } from 'react';

class Button extends Component{
  render() {
    var title = this.props.title;
    return (
      <button className="uk-button uk-button-primary">
        <span>{title}</span>
      </button>
    );
  }
}

export default Button;