/**
*
* UserCard
*
*/

import React from 'react';
import {Card, Row, Col, Button, message, Avatar} from 'antd';
import {Link} from "react-router-dom";


class UserCard extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props)
  }

  render() {
    return (
      <Card
        hoverable
        className='user-card'
        style={{width: '100%', cursor: 'default'}}
        bodyStyle={{padding: '14px 22px'}}
      >
        <Row type='flex' align='middle'>
          <Col className='md-margin-bottom-14' xs={{span: 24}} md={{span: 14}} lg={{span: 24}} xl={{span: 14}} style={{display: 'flex'}}>
            <Avatar
              src={this.props.avatarLink}
              style={{minHeight: 50, minWidth: 50, marginRight: 20, borderRadius: '50%'}}
            >
            </Avatar>
            <div style={{display: 'inline', fontSize: 16}}>
              <div style={{color: '#000'}}>{this.props.name}</div>
              <div className='word-break'>{this.props.email}</div>
            </div>
          </Col>
          <Col className='md-center-container user-card-profile-btn' xs={{span: 24}} md={{span: 10}} lg={{span: 24}} xl={{span: 10}} style={{textAlign: 'right'}}>
            <Link to={`/profile/${this.props.id}`}><Button>Перейти к профилю</Button></Link>
          </Col>
        </Row>
      </Card>
    );
  }
}

UserCard.propTypes = {

};

export default UserCard;
