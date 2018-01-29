/**
*
* Chat
*
*/

import React from 'react';
import ChatRoom from './ChatRoom';


class Chat extends React.Component { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <div>
        <ChatRoom {...this.props}/>
      </div>
    );
  }
}

export default Chat;
