import React, { Component } from 'react';

class Button extends Component{
  render() {
    var title = this.props.title;
    return (
      <button className="uk-button uk-button-danger">{title}</button>
    );
  }
}

export default Button;