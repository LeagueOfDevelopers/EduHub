import React from 'react';
import PropTypes from 'prop-types';
import { parseJwt } from "../../globalJS";

export default class Message extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      this.props.message ?
        <li className={`message ${parseJwt(localStorage.getItem('token')).UserId == this.props.message.senderId ? "right" : "left"}`}>
          {parseJwt(localStorage.getItem('token')).UserId != this.props.message.senderId
          && <span style={{opacity: 0.8}}>{this.props.message.senderName}</span>
          }
          <p style={{margin: '6px 0'}}>{this.props.message.text}</p>
          <div style={{textAlign: 'left', fontSize: 12, opacity: 0.5, marginBottom: 6}}>
            {(new Date(this.props.message.sentOn).getHours() < 10 ? '0' + new Date(this.props.message.sentOn).getHours() : new Date(this.props.message.sentOn).getHours()) + ':' + (new Date(this.props.message.sentOn).getMinutes() < 10 ? '0' + new Date(this.props.message.sentOn).getMinutes() : new Date(this.props.message.sentOn).getMinutes())}
          </div>
        </li>
        :
        null
    )
  }
}

Message.propTypes = {
  user: PropTypes.string,
  username: PropTypes.string,
  content: PropTypes.string,
  time: PropTypes.string
};
