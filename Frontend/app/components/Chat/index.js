/**
*
* Chat
*
*/

import React from 'react';
// import styled from 'styled-components';
import ChatRoom from './ChatRoom';


class Chat extends React.Component { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <div>
        <ChatRoom/>
      </div>
    );
  }
}

Chat.propTypes = {

};

export default Chat;
