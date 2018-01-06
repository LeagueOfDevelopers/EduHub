/**
 *
 * GroupPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';

import {selectGroupPage} from './selectors';
import reducer from './reducer';
import saga from './saga';
import {Col, Row, Button, message, Dropdown, Input, Menu} from 'antd';
// import styled from 'styled-components';

import config from '../../config'

import MemberList from 'components/MembersList/Loadable';
import Chat from 'components/Chat/Loadable';
import {Link} from "react-router-dom";
import * as ReactDOM from "react-dom";

export class GroupPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      isPrivate: true,
      hasTeacher: false,
      name: "",
      description: "",
      isActive: true,
      tags: [],
      members: [],
      totalValue: 0,
      size: 0,
      type: "",
      isInGroup: false,
      inviteVisible: false
    };

    this.onSetResult = this.onSetResult.bind(this);
    this.inviteMember = this.inviteMember.bind(this);
    this.leaveGroup = this.leaveGroup.bind(this);
    this.enterGroup = this.enterGroup.bind(this);
    this.handleVisibleChange = this.handleVisibleChange.bind(this);
  }

  handleVisibleChange = (flag) => {
    this.setState({ inviteVisible: flag });
  };

  inviteMenu = (
    <Menu>
      <Menu.Item className='unhover' key="0">
        <Input onPressEnter={this.inviteMember} ref={input => this.inviteInput = input} placeholder='Введите id пользователя'/>
      </Menu.Item>
    </Menu>
  );

  componentDidMount() {
    fetch(`${config.API_BASE_URL}/group/1`, {
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    })
      .then(response => {
        if(response.ok) {
          return response.json();
        }
        else throw new Error('Не удалось получить группу');
      })
      .then(result => this.onSetResult(result));

    this.setState({isInGroup: this.state.members.find(member => member.name === localStorage.getItem('name'))})
  }

  onSetResult(result) {
    this.setState({
      name: result.name,
      members: result.members,
      isActive: result.isActive,
      size: result.size,
      totalValue: result.totalValue,
      // type: result.type,
      tags: result.tags,
      description: result.description,
      isPrivate: result.isPrivate,
      hasTeacher: result.hasTeacher,
    })
  }

  inviteMember() {
    if(ReactDOM.findDOMNode(this.inviteInput).value !== '') {
      fetch(`${config.API_BASE_URL}/group/1/member/${localStorage.getItem('name')}/invite/${ReactDOM.findDOMNode(this.inviteInput).value}`, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
        .then(response => {
          if(response.ok) {
            return response.json();
          }
          else throw new Error('Не удалось пригласить пользователя');
        })
    }
  }

  leaveGroup() {
    this.setState({isInGroup: false})
  }

  enterGroup() {
    this.setState({isInGroup: true})
  }

  render() {
    return (
      !this.state.isActive ?
        (<div>Данной группы не существует</div>)
        : (
          <div>
            <Col span={20} offset={2} style={{marginTop: 40, marginBottom: 60, fontSize: 16}} className='md-center-container'>
              <Col className='md-offset-16px' md={{span: 10}} lg={{span: 7}}>
                <Row style={{width: 248}}>
                  <Row style={{marginBottom: 26}}>
                    <h3 style={{margin: 0, fontSize: 22}}>{this.state.name}</h3>
                    { this.state.hasTeacher ?
                      (<span style={{color: 'rgba(0,0,0,0.6)'}}>Преподаватель найден</span>)
                      : (<span style={{color: 'rgba(0,0,0,0.6)'}}>Идет поиск преподавателя</span>)
                    }
                  </Row>
                  <Row gutter={6} type='flex' justify='start' style={{marginBottom: 8}}>
                    {this.state.tags.map((item) =>
                      <Link to="#">{item}</Link>
                    )}
                  </Row>
                  <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
                    <Col>Формат</Col>
                    <Col>{this.state.type}</Col>
                  </Row>
                  <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
                    <Col>Стоимость</Col>
                    <Col>{this.state.totalValue} руб.</Col>
                  </Row>
                  <Row type='flex' justify='flex-start' style={{marginBottom: 12}}>
                    { this.state.isPrivate ?
                      (<Col>Эта группа является приватной</Col>)
                      : (<Col>Эта группа не является приватной</Col>)
                    }
                  </Row>
                </Row>
                <Row style={{marginLeft: -16, marginBottom: 20}}>
                  <MemberList members={this.state.members} size={this.state.size}/>
                </Row>
                <Row className='md-center-container'>
                  <Dropdown.Button
                    overlay={this.inviteMenu}
                    onVisibleChange={this.handleVisibleChange}
                    visible={this.state.inviteVisible}
                    trigger={['click']}
                    size='large'
                    style={{width: 280, marginLeft: -16}}
                    type='primary'
                    onClick={this.inviteMember}
                  >
                    Пригласить
                  </Dropdown.Button>
                </Row>
              </Col>
              <Col sm={{span: 24}} md={{span: 13, offset: 1}} lg={{span: 16, offset: 1}}>
                <Row className='md-center-container' style={{textAlign: 'right', marginTop: 8}}>
                  { this.state.isInGroup ?
                    (<Button onClick={this.leaveGroup}>Покинуть группу</Button>)
                    : (<Button type='primary' onClick={this.enterGroup}>Вступить в группу</Button>)
                  }
                </Row>
                <Row style={{marginTop: 42}}>
                  <Col><h3 style={{margin: 0, fontSize: 18}}>Описание</h3></Col>
                </Row>
                <Row style={{marginBottom: 40}}>
                  <p>
                    {this.state.description}
                  </p>
                </Row>
                <Row>
                  <Chat isInGroup={this.state.isInGroup}/>
                </Row>
              </Col>
            </Col>
          </div>
        )
    );
  }
}

GroupPage.propTypes = {
  name: PropTypes.string,
  isActive: PropTypes.bool,
  tags: PropTypes.array,
  type: PropTypes.string,
  totalValue: PropTypes.number,
  // private: PropTypes.bool,
  inviteMember: PropTypes.func,
  leaveGroup: PropTypes.func,
  description: PropTypes.string
};

const mapStateToProps = createStructuredSelector({
});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'groupPage', reducer });
const withSaga = injectSaga({ key: 'groupPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(GroupPage);
