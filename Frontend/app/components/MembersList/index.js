/**
*
* MembersList
*
*/

import React from 'react';
// import styled from 'styled-components';
import { List, Avatar, Icon, Popconfirm, message, Row, Col } from 'antd';
import {Link} from "react-router-dom";
import PropTypes from 'prop-types';


class MembersList extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.confirm = this.confirm.bind(this);
  }

  confirm() {
    message.error('Участник удален')
  }

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
                  description={item.member.memberRole}
                />
                {item.role!=='Создатель' ?
                  <Popconfirm title='Удалить участника?' onConfirm={this.confirm} okText="Да" cancelText="Нет">
                    <Icon
                      style={{fontSize: 18, cursor: 'pointer'}}
                      type="close"
                    />
                  </Popconfirm>
                  : ''}

              </List.Item>
            )}
          >
          </List>
        </div>
      </div>
    );
  }
}

MembersList.defaultProps = {
  name: '',
  count: 0,
  size: 0,
  role: '',
  members: [],
  id: ''
};

MembersList.propTypes = {
  name: PropTypes.string,
  count: PropTypes.number,
  size: PropTypes.number,
  role: PropTypes.string,
  id: PropTypes.array,
  members: PropTypes.array
};

export default MembersList;
