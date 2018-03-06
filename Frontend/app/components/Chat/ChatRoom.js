import React from 'react';
import ReactDom from 'react-dom';
import PropTypes from 'prop-types';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { sendMessage } from "../../containers/GroupPage/actions";
import {Input} from 'antd';
import Message from './Message';

class ChatRoom extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      messages: [],
      showIsInGroupError: false
    };
  }

  componentDidMount() {
    this.scrollToBottom();
  }

  componentDidUpdate() {
    this.scrollToBottom()
  }

  scrollToBottom() {
    ReactDom.findDOMNode(this.chat).scrollTop = ReactDom.findDOMNode(this.chat).scrollHeight;
  }

  submitMessage(e) {
    e.preventDefault();

    if(!this.props.isInGroup) {
      this.setState({showIsInGroupError: true})
    }
    else {
      this.setState({showIsInGroupError: false})
    }

    (ReactDom.findDOMNode(this.msgInput).value !== '' && this.props.isInGroup) ?
      localStorage.getItem('withoutServer') === 'true' ?
        this.setState({
          messages: this.state.messages.concat([
            {
              id: Math.random(),
              username: localStorage.getItem('name'),
              content: ReactDom.findDOMNode(this.msgInput).value,
              time: new Date().getHours() + ':' + (new Date().getMinutes()<10 ? '0' : '') + new Date().getMinutes()
            }
          ])
        })
        : this.props.sendMessage(this.props.groupId, ReactDom.findDOMNode(this.msgInput).value)
      : null;

    ReactDom.findDOMNode(this.msgInput).value = '';
  }

  render() {
    return (
      <div className='chatroom'>
        <div className='header'>Чат</div>
        <ul className='chat' ref={chat => this.chat = chat}>
          {localStorage.getItem('withoutServer') === 'true' ?
            this.state.messages.map(msg =>
              <Message key={msg.id} message={msg} user={localStorage.getItem('name')}/>
            )
            :
            this.props.chat ?
              this.props.chat.map(msg =>
                <Message key={msg.id} message={msg}/>
            )
              : null
          }
        </ul>
        <form className='input' onSubmit={event => this.submitMessage(event)}>
          <Input size='large' ref={input => this.msgInput = input} placeholder='Введите сообщение' />
        </form>
        {this.state.showIsInGroupError ?
          (<div style={{color: 'red'}}>Вы должны вступить в группу</div>)
          : ''
        }
      </div>
    )
  }

}

ChatRoom.propTypes = {
  scrollToBottom: PropTypes.func,
  submitMessage: PropTypes.func,
  username: PropTypes.string,
  content: PropTypes.string,
  time: PropTypes.string,
  messages: PropTypes.array,
  senderId: PropTypes.string
};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    sendMessage: (groupId, text) => dispatch(sendMessage(groupId, text))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ChatRoom)
