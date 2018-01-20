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

// import {selectGroupPage} from './selectors';
import reducer from './reducer';
import saga from './saga';
import {Col, Row, Button, message, Dropdown, Input, Menu} from 'antd';
// import styled from 'styled-components';

import config from '../../config'

import MemberList from 'components/MembersList/Loadable';
import Chat from 'components/Chat/Loadable';
import {Link} from "react-router-dom";
import * as ReactDOM from "react-dom";

import {parseJwt} from "../../globaljs";

export class GroupPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      id: this.props.match.params.id,
      isPrivate: true,
      teacher: null,
      title: "Название группы",
      description: "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. ",
      isActive: true,
      tags: ['js', 'c#'],
      members: [
        {
          member: {
            userId: "848a3202-7085-4cba-842f-07d07eff7b35",
            memberRole: "Создатель",
            paid: true,
            acceptedCourse: false
          },
          name: "Первый пользователь",
          avatarLink: "string"
        },
        {
          member: {
            userId: "string",
            memberRole: "Участник",
            paid: true,
            acceptedCourse: false
          },
          name: "Второй пользователь",
          avatarLink: "string"
        }
      ],
      moneyPerUser: 600,
      size: 10,
      groupType: "Лекция",
      isInGroup: false,
      inviteVisible: false,
      userData: localStorage.getItem('token') ? parseJwt(localStorage.getItem('token')) : null
    };

    this.onSetResult = this.onSetResult.bind(this);
    this.inviteMember = this.inviteMember.bind(this);
    this.leaveGroup = this.leaveGroup.bind(this);
    this.enterGroup = this.enterGroup.bind(this);
    this.handleVisibleChange = this.handleVisibleChange.bind(this);
    this.tryInvite = this.tryInvite.bind(this);
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
    if(!(localStorage.getItem('without_server') === 'true')) {
      fetch(`${config.API_BASE_URL}/group/${this.state.id}`, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
        .then(response => response.json())
        .then(result => this.onSetResult(result))
        .catch(error => error);
    }

    this.setState({isInGroup: this.state.members.find(item => item.member.userId === this.state.userData.UserId)});
  }

  onSetResult(result) {
    this.setState({
      title: result.groupInfo.title,
      members: result.members,
      isActive: result.groupInfo.isActive,
      size: result.groupInfo.size,
      moneyPerUser: result.groupInfo.moneyPerUser,
      groupType: result.groupInfo.groupType,
      tags: result.groupInfo.tags,
      description: result.groupInfo.description,
      isPrivate: result.groupInfo.isPrivate,
      teacher: result.teacher,
    })
  }

  tryInvite() {
    if(this.state.inviteVisible && ReactDOM.findDOMNode(this.inviteInput).value !== '') {
      this.inviteMember();
      ReactDOM.findDOMNode(this.inviteInput).value = '';
    }
  }

  inviteMember() {
    if(localStorage.getItem('without_server') === 'true') {
      message.success('Приглашение отправлено')
    }
    else {
      fetch(`${config.API_BASE_URL}/group/${this.state.id}/member/invite/${ReactDOM.findDOMNode(this.inviteInput).value}`, {
        headers: {
          'Method': 'POST',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
        .catch(error => error)
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
                    <h3 style={{margin: 0, fontSize: 22}}>{this.state.title}</h3>
                    { this.state.teacher ?
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
                    <Col>{this.state.groupType}</Col>
                  </Row>
                  <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
                    <Col>Стоимость</Col>
                    <Col>{this.state.moneyPerUser} руб.</Col>
                  </Row>
                  <Row type='flex' justify='flex-start' style={{marginBottom: 12}}>
                    { this.state.isPrivate ?
                      (<Col>Эта группа является приватной</Col>)
                      : (<Col>Эта группа не является приватной</Col>)
                    }
                  </Row>
                </Row>
                <Row style={{marginLeft: -16, marginBottom: 20}}>
                  <MemberList members={this.state.members} size={this.state.size} isInGroup={this.state.isInGroup}/>
                </Row>
                {
                  this.state.isInGroup ?
                    this.state.members.find(item =>
                      item.member.userId === this.state.userData.UserId)
                        .member.memberRole === 'Создатель' ?
                      (<Row className='md-center-container'>
                        <Dropdown
                          overlay={this.inviteMenu}
                          onVisibleChange={this.handleVisibleChange}
                          visible={this.state.inviteVisible}
                          trigger={['click']}
                        >
                          <Button
                            size='large'
                            style={{width: 280, marginLeft: -16}}
                            type='primary'
                            onClick={this.tryInvite}
                          >
                            Пригласить
                          </Button>
                        </Dropdown>
                      </Row>) : null
                    : null
                }
              </Col>
              <Col sm={{span: 24}} md={{span: 13, offset: 1}} lg={{span: 16, offset: 1}}>
                <Row className='md-center-container' style={{textAlign: 'right', marginTop: 8}}>
                  { this.state.isInGroup ?
                    (<Button onClick={this.leaveGroup}>Покинуть группу</Button>)
                    : (<Button type='primary' onClick={this.enterGroup}>Вступить в группу</Button>)
                  }
                </Row>
                <Row style={{marginTop: 42}}>
                  <Col><h3 style={{fontSize: 18}}>Описание</h3></Col>
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
  title: PropTypes.string,
  isActive: PropTypes.bool,
  tags: PropTypes.array,
  groupType: PropTypes.string,
  moneyPerUser: PropTypes.number,
  inviteMember: PropTypes.func,
  leaveGroup: PropTypes.func,
  description: PropTypes.string,
  isPrivate: PropTypes.bool,
  teacher: PropTypes.object,
  members: PropTypes.array,
  size: PropTypes.number,
  isInGroup: PropTypes.bool,
  inviteVisible: PropTypes.bool,
  groupId: PropTypes.number
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
