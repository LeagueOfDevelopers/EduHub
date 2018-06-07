import React from 'react';
import ReactDom from 'react-dom';
import PropTypes from 'prop-types';
import { createStructuredSelector } from 'reselect';
import { connect } from 'react-redux';
import { sendMessage, getCurrentChat, getMessage } from "../../containers/GroupPage/actions";
import { connectSockets } from "../../globalJS";
import {Input, Avatar, Row, Col} from 'antd';
import Message from './Message';

class ChatRoom extends React.Component {
  constructor(props) {
    super(props);

    const uri = `ws://85.143.104.47:2411/api/sockets/creation?token=${localStorage.getItem('token')}`;
    localStorage.getItem('token') ? this.socket = new WebSocket(uri) : null;

    this.state = {
      messages: [],
      showIsInGroupError: false
    };
  }

  componentDidMount() {
    localStorage.getItem('token') ? this.connectSocket(this.socket) : null;
    this.scrollToBottom();
  }

  connectSocket(socket) {
    const _this = this;
    socket.onopen = function(event) {
      console.log("opened connection");
    };
    socket.onclose = function(event) {
      console.log("closed connection");
    };
    socket.onmessage = function(event) {
      // _this.props.getCurrentChat(_this.props.groupId);
      let msgData = JSON.parse(event.data);
      console.log('message received ' + JSON.stringify(msgData));
      _this.props.getMessage({
        id: msgData.Id,
        senderId: msgData.SenderId,
        senderName: msgData.SenderName,
        sentOn: msgData.SentOn,
        text: msgData.Text
      });
    };
    socket.onerror = function(event) {
      console.log("error " + JSON.stringify(event));
    };
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevProps.chat && this.props.chat && (prevProps.chat.length !== this.props.chat.length) || !prevProps.chat && this.props.chat) {
      this.scrollToBottom()
    }

    if(prevProps.isInGroup !== this.props.isInGroup) {
      if(this.props.isInGroup) {
        this.setState({showIsInGroupError: false})
      }
    }
  }

  componentWillUnmount() {
    localStorage.getItem('token') ? this.socket.close() : null
  }

  scrollToBottom() {
    ReactDom.findDOMNode(this.chat).scrollTop = ReactDom.findDOMNode(this.chat).scrollHeight;
  }

  submitMessage(e) {
    e ? e.preventDefault() : null;

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
              text: ReactDom.findDOMNode(this.msgInput).value,
              time: new Date().getHours() + ':' + (new Date().getMinutes()<10 ? '0' : '') + new Date().getMinutes()
            }
          ])
        })
        : this.props.sendMessage(this.props.groupId, ReactDom.findDOMNode(this.msgInput).value)
      : null;

    ReactDom.findDOMNode(this.msgInput).value = '';
    ReactDom.findDOMNode(this.msgInput).focus();
  }

  render() {
    return (
      <div>
        <div className='chatroom'>
          <div className='header'>Чат</div>
          <div style={{overflowX: 'hidden'}}>
            <ul className='chat' ref={chat => this.chat = chat}>
              {localStorage.getItem('withoutServer') === 'true' ?
                this.state.messages.map(msg =>
                  <Message key={msg.notificationType ? `notify-${msg.id}` : `msg-${msg.id}`} message={msg} user={localStorage.getItem('name')}/>
                )
                :
                this.props.chat ?
                  this.props.chat.map((msg, index) =>
                    <Message key={
                      msg.id ?
                        msg.notificationType ?
                          `notify-${msg.id}`
                          : `msg-${msg.id}`
                        : Math.random()
                    }
                             message={msg}
                    />
                  )
                  :
                  [].map((msg, index) =>
                    <Message key={index}/>
                  )
              }
            </ul>
            <form className='input' onSubmit={event => this.submitMessage(event)}>
              <Row type='flex'>
                <Col style={{width: 'calc(100% - 44px)'}}>
                  <Input size='large' style={{width: '100%'}} ref={input => this.msgInput = input} placeholder='Введите сообщение' />
                </Col>
                <Col style={{display: 'flex', height: 40, alignItems: 'center', justifyContent: 'flex-end', minWidth: 24, marginRight: 20}}>
                  <img src={require('../../images/send-darkblue.svg')} onClick={() => this.submitMessage()} className='send-msg-btn'/>
                </Col>
              </Row>
            </form>
          </div>
        </div>
        {this.state.showIsInGroupError ?
          (<div style={{color: 'red', marginTop: 4}}>Вы должны вступить в группу</div>)
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
    sendMessage: (groupId, text) => dispatch(sendMessage(groupId, text)),
    getCurrentChat: (groupId) => dispatch(getCurrentChat(groupId)),
    getMessage: (msgData) => dispatch(getMessage(msgData))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(ChatRoom)
