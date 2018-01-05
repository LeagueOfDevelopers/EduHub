import React from 'react';
import PropTypes from 'prop-types';
import {Avatar} from 'antd';

export default class Message extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <li className={`message ${this.props.user === this.props.message.username ? "right" : "left"}`}>
        {this.props.user !== this.props.message.username
        && <span style={{opacity: 0.8}}>{this.props.message.username}</span>
        }
        <p style={{margin: '6px 0'}}>{this.props.message.content}</p>
        <div style={{textAlign: 'right', fontSize: 14, opacity: 0.5}}>{this.props.message.time}</div>
      </li>
    )
  }
}

Message.propTypes = {
  user: PropTypes.string,
  username: PropTypes.string,
  content: PropTypes.string,
  time: PropTypes.string
}
