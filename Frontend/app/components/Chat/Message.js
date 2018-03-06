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
        <li className={`message ${parseJwt(localStorage.getItem('token')).UserId === this.props.message.senderId ? "right" : "left"}`}>
          {parseJwt(localStorage.getItem('token')).UserId !== this.props.message.senderId
          && <span style={{opacity: 0.8}}>{this.props.message.senderId}</span>
          }
          <p style={{margin: '6px 0'}}>{this.props.message.text}</p>
          <div style={{textAlign: 'right', fontSize: 14, opacity: 0.5}}>{this.props.message.sentOn}</div>
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
