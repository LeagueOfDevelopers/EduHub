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
import reducer from './reducer';
import saga from './saga';
import { enterGroup, leaveGroup} from "./actions";
import {Link} from "react-router-dom";
import config from "../../config";
import {getGroupType, parseJwt, getMemberRole} from "../../globalJS";
import {Col, Row, Button, message} from 'antd';
import MemberList from '../../components/MembersList/Loadable';
import Chat from '../../components/Chat/Loadable';
import InviteMemberSelect from '../../components/InviteMemberSelect/Loadable';
import SigningInForm from "../../containers/SigningInForm/index";

const groupData = {
    groupInfo: {
      isPrivate: true,
      title: "Название группы",
      description: "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. Описание группы. " +
      "Описание группы. Описание группы. ",
      isActive: true,
      tags: ['js', 'c#'],
      moneyPerUser: 600,
      size: 10,
      groupType: "Лекция",
    },
    members: [
      {
        member: {
          userId: "848a3202-7085-4cba-842f-07d07eff7b35",
          memberRole: 3,
          paid: true,
          acceptedCourse: false
        },
        name: "Первый пользователь",
        avatarLink: "string"
      },
      {
        member: {
          userId: "string",
          memberRole: 1,
          paid: true,
          acceptedCourse: false
        },
        name: "Второй пользователь",
        avatarLink: "string"
      }
      ],
    educator: null,
  };

export class GroupPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      id: this.props.match.params.id,
      groupData: {
        groupInfo: {
          isPrivate: true,
          title: '',
          description: '',
          isActive: true,
          tags: [],
          cost: null,
          size: null,
          groupType: '',
        },
        members: [],
        educator: null,
      },
      userData: localStorage.getItem('token') ? parseJwt(localStorage.getItem('token')) : null,
      isInGroup: false,
      isCreator: false,
      needUpdate: false,
      signInVisible: false,
    };

    this.onSetResult = this.onSetResult.bind(this);
    this.getCurrentGroup = this.getCurrentGroup.bind(this);
    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  onSignInClick = () => {
    this.setState({signInVisible: true})
  };

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  getCurrentGroup = () => {
    if(localStorage.getItem('without_server') !== 'true') {
      fetch(`${config.API_BASE_URL}/group/${this.state.id}`, {
        headers: {
          'Content-Type': 'application/json-patch+json',
          // 'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
        .then(response => response.json())
        .then(result => this.onSetResult(result))
        .catch(error => error)
    }
    else {
      this.onSetResult(groupData)
    }
  };

  componentDidMount() {
    this.getCurrentGroup()
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevState.needUpdate !== this.state.needUpdate) {
      this.getCurrentGroup();
      this.setState({needUpdate: false});
    }
  }

  onSetResult(result) {
    this.setState({
      groupData: {
        groupInfo: result.groupInfo,
        members: result.members,
        educator: result.educator
      },
      isInGroup: this.state.userData ?
        Boolean(result.members.find(item => item.userId === this.state.userData.UserId)) : false,
      });
    this.setState({
      isCreator: this.state.isInGroup ?
        getMemberRole(result.members.find(item =>
          item.userId === this.state.userData.UserId).role) === 'Создатель' : false });
  }

  render() {
    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40, marginBottom: 160, fontSize: 16}} className='md-center-container'>
          <Col className='md-offset-16px' md={{span: 10}} lg={{span: 7}}>
            <Row style={{width: 248}}>
              <Row style={{marginBottom: 26}}>
                <h3 style={{margin: 0, fontSize: 22}}>{this.state.groupData.groupInfo.title}</h3>
                { this.state.groupData.educator ?
                  (<span style={{color: 'rgba(0,0,0,0.6)'}}>Преподаватель найден</span>)
                  : (<span style={{color: 'rgba(0,0,0,0.6)'}}>Идет поиск преподавателя</span>)
                }
              </Row>
              <Row gutter={6} type='flex' justify='start' style={{marginBottom: 8}}>
                {this.state.groupData.groupInfo.tags.map((item) =>
                  <Link key={item} to="#">{item}</Link>
                )}
              </Row>
              <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
                <Col>Формат</Col>
                <Col>{getGroupType(this.state.groupData.groupInfo.groupType)}</Col>
              </Row>
              <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
                <Col>Стоимость</Col>
                <Col>{this.state.groupData.groupInfo.cost} руб.</Col>
              </Row>
              <Row type='flex' justify='flex-start' style={{marginBottom: 12}}>
                {this.state.groupData.groupInfo.isPrivate ?
                  (<Col>Эта группа является приватной</Col>)
                  : (<Col>Эта группа не является приватной</Col>)
                }
              </Row>
            </Row>
            <Row style={{marginLeft: -16, marginBottom: 20}}>
              <MemberList members={this.state.groupData.members} size={this.state.groupData.groupInfo.size} isCreator={this.state.isCreator}/>
            </Row>
            <Row>
              {this.state.isCreator ?
                (<Row className='md-center-container'>
                  <InviteMemberSelect groupId={this.state.id}/>
                </Row>) : null
              }
            </Row>
          </Col>
          <Col xs={{span: 24}} md={{span: 13, offset: 1}} lg={{span: 15, offset: 2}} xl={{span: 16, offset: 1}}>
            <Row className='md-center-container' style={{textAlign: 'right', marginTop: 8}}>
              {this.state.isInGroup ?
                (<Button onClick={() => {
                  this.setState({needUpdate: true});
                  this.props.leaveGroup(this.state.id, this.state.userData.UserId)
                }}
                >
                  Покинуть группу
                </Button>)
                : (<Button type='primary' onClick={() => {
                  if(this.state.userData) {
                    this.props.enterGroup(this.state.id);
                    this.setState({needUpdate: true});
                  }
                  else {
                    this.onSignInClick()
                  }
                }}
                >
                  Вступить в группу
                </Button>)
              }
            </Row>
            <Row>
              <Row style={{marginTop: 42}}>
                <Col><h3 style={{fontSize: 18}}>Описание</h3></Col>
              </Row>
              <Row style={{marginBottom: 40}}>
                <p>
                  {this.state.groupData.groupInfo.description}
                </p>
              </Row>
            </Row>
            <Row style={{width: '100%'}}>
              <Chat isInGroup={this.state.isInGroup}/>
            </Row>
          </Col>
        </Col>
        <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
      </div>
    );
  }
}

GroupPage.propTypes = {
  title: PropTypes.string,
  isActive: PropTypes.bool,
  tags: PropTypes.array,
  groupType: PropTypes.string,
  cost: PropTypes.number,
  inviteMember: PropTypes.func,
  leaveGroup: PropTypes.func,
  description: PropTypes.string,
  isPrivate: PropTypes.bool,
  educator: PropTypes.object,
  members: PropTypes.array,
  size: PropTypes.number,
  isInGroup: PropTypes.bool,
  inviteVisible: PropTypes.bool,
  groupId: PropTypes.number
};

GroupPage.defaultProps = {
  users: localStorage.getItem('withoutServer') === 'true' ?
    ['Первый пользователь', 'Второй пользователь'] : []
};

const mapStateToProps = createStructuredSelector({
});

function mapDispatchToProps(dispatch) {
  return {
    enterGroup: (groupId) => dispatch(enterGroup(groupId)),
    leaveGroup: (groupId, memberId) => dispatch(leaveGroup(groupId, memberId))
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
