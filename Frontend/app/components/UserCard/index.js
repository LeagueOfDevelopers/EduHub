/**
*
* UserCard
*
*/

import React from 'react';
import {Card, Row, Col, Button, message, Avatar} from 'antd';
import {Link} from "react-router-dom";
import {getGender, getAge} from "../../globalJS";


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
          <Col className='md-margin-bottom-14' xs={{span: 24}} md={{span: 14}} lg={{span: 24}} xl={{span: 14}} style={{display: 'flex', alignItems: 'center'}}>
            <Avatar
              src={this.props.avatarLink}
              style={{minHeight: 50, minWidth: 50, marginRight: 20, borderRadius: '50%'}}
            >
            </Avatar>
            <div>
              <div style={{color: '#000', fontSize: 16, display: 'flex', flexWrap: 'wrap', alignItems: 'center'}}>
                <span style={{marginRight: 14}}>{this.props.name}</span>
                <span title='Пользователь является преподавателем' style={{opacity: 0.4, fontSize: 14}}>{this.props.isTeacher ? 'Преподаватель' : ''}</span>
              </div>
              <div style={{display: 'flex', flexWrap: 'wrap', alignItems: 'center'}}>
                <span title='Пол пользователя' style={{opacity: 0.5, marginRight: 24, fontSize: 14}}>{getGender(this.props.gender)}</span>
                <span style={{opacity: 0.5, fontSize: 14}}>
                  {this.props.birthYear ?
                    `${getAge(this.props.birthYear)}
                      ${getAge(this.props.birthYear) % 10 === 1 ? 'год'
                      : getAge(this.props.birthYear) % 10 > 1 && getAge(this.props.birthYear) % 10 < 5 ? 'года'
                        : getAge(this.props.birthYear) % 10 > 4 || getAge(this.props.birthYear) % 10 === 0 ? 'лет' : ''}`
                          : ''}
                </span>
              </div>
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
