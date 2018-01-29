/**
*
* MembersList
*
*/

import React from 'react';
import PropTypes from 'prop-types';
import {parseJwt, getMemberRole} from "../../globalJS";
import {Link} from "react-router-dom";
import { List, Avatar, Icon, Popconfirm, message, Row, Col } from 'antd';

class MembersList extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.confirm = this.confirm.bind(this);
  }

  confirm = () => {
    message.error('Участник удален')
  };

  render() {
    return (
      <div style={{width: 280, boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px'}}>
        <Row type='flex' justify='space-between' style={{padding: '6px 16px', boxShadow: '0px 2px 6px -2px rgba(0,0,0,0.36)'}}>
          <Col>Участников</Col>
          <Col>{this.props.members.length + '/' + this.props.size}</Col>
        </Row>
        <div className="member-container">
          <List
            dataSource={this.props.members}
            renderItem={item => (
              <List.Item key={item.id}>
                <List.Item.Meta
                  avatar={
                    <Avatar
                      src={item.avatarLink}
                    />}
                  title={<Link to="#">{item.name}</Link>}
                  description={getMemberRole(item.memberRole)}
                />
                { localStorage.getItem('token') && this.props.isInGroup &&
                  getMemberRole(this.props.members.find(item =>
                    item.userId === parseJwt(localStorage.getItem('token')).UserId).memberRole) === 'Создатель' &&
                    getMemberRole(item.memberRole) !== 'Создатель' ?
                      (<Popconfirm title='Удалить участника?' onConfirm={this.confirm} okText="Да" cancelText="Нет">
                        <Icon
                          style={{fontSize: 18, cursor: 'pointer'}}
                          type="close"
                        />
                      </Popconfirm>)
                  : null
                }
              </List.Item>
            )}
          >
          </List>
        </div>
      </div>
    );
  }
}

MembersList.propTypes = {
  name: PropTypes.string,
  length: PropTypes.number,
  size: PropTypes.number,
  role: PropTypes.string,
  id: PropTypes.array,
  members: PropTypes.array
};

export default MembersList;
