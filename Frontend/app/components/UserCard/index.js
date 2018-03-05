/**
*
* UserCard
*
*/

import React from 'react';
import {Card, Row, Col, Button, message, Avatar} from 'antd';


class UserCard extends React.Component { // eslint-disable-line react/prefer-stateless-function
  render() {
    return (
      <Card
        hoverable
        className='user-card'
        style={{width: '100%', cursor: 'default'}}
        bodyStyle={{padding: '14px 22px'}}
      >
        <Row type='flex' align='middle'>
          <Col className='md-margin-bottom-14' xs={{span: 24}} md={{span: 16}} lg={{span: 24}} xl={{span: 16}} style={{display: 'flex'}}>
            <Avatar
              src={this.props.avatarLink}
              style={{minHeight: 50, minWidth: 50, marginRight: 20, borderRadius: '50%'}}
            >
            </Avatar>
            <div style={{display: 'inline', fontSize: 16}}>
              <div style={{color: '#000'}}>{this.props.name}</div>
              <div className='word-break'>{this.props.mail}</div>
            </div>
          </Col>
          <Col className='md-center-container user-card-profile-btn' xs={{span: 24}} md={{span: 8}} lg={{span: 24}} xl={{span: 8}} style={{textAlign: 'right'}}>
            <Button>Перейти к профилю</Button>
          </Col>
        </Row>
      </Card>
    );
  }
}

UserCard.propTypes = {

};

export default UserCard;
