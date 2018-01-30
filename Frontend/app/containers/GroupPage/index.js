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
import { makeSelectGroupData } from "./selectors";
import { getGroupData, enterGroup, leaveGroup } from "./actions";
import config from '../../config'
import {Link} from "react-router-dom";
import {parseJwt, getGroupType, getMemberRole} from "../../globalJS";
import {Col, Row, Button, message, Dropdown, Menu, Select} from 'antd';
import MemberList from '../../components/MembersList/Loadable';
import Chat from '../../components/Chat/Loadable';
import { withRouter } from 'react-router-dom'

const defaultGroupInfo = {
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
  teacher: null,
};

const defaultSelectData = ['Первый пользователь', 'Второй пользователь'];

export class GroupPage extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      id: this.props.match.params.id,
      title: '',
      isActive: true,
      size: 0,
      moneyPerUser: 0,
      groupType: '',
      tags: [],
      description: '',
      isPrivate: null,
      members: [],
      teacher: null,
      isInGroup: false,
      inviteVisible: false,
      userData: localStorage.getItem('token') ? parseJwt(localStorage.getItem('token')) : null,
      selectData: [],
      selectValue: ''
    };

    this.onSetResult = this.onSetResult.bind(this);
    this.inviteMember = this.inviteMember.bind(this);
    this.handleVisibleChange = this.handleVisibleChange.bind(this);
    this.fetchData = this.fetchData.bind(this);
    this.handleSelectChange = this.handleSelectChange.bind(this);
  }

  inviteMenu = (options) => (
      <Menu>
        <Menu.Item className='unhover' key="0">
          <Select
            mode="combobox"
            style={{width: '100%'}}
            value={this.state.selectValue}
            placeholder='Введите имя пользователя'
            defaultActiveFirstOption={false}
            showArrow={false}
            notFoundContent='Нет совпадений'
            onSelect={this.inviteMember}
            onChange={this.handleSelectChange}
          >
            {options.map(item => <Select.Option key={item}>{item}</Select.Option>)}
          </Select>
        </Menu.Item>
      </Menu>
    );

  componentDidMount() {
    if(localStorage.getItem('without_server') === 'true') {
      this.onSetResult(defaultGroupInfo)
    }
    else {
      this.props.getCurrentGroupData(this.state.id);
      setTimeout(() => this.onSetResult(this.props.currentGroupData), 1000)
    }
  }

  // componentDidUpdate(prevProps, prevState) {
  //   if(prevState !== this.state) {
  //     this.props.getCurrentGroupData(this.state.id);
  //     setTimeout(() => this.onSetResult(this.props.currentGroupData), 1000)
  //   }
  // }

  handleVisibleChange = (flag) => {
    this.setState({ inviteVisible: flag });
  };

  fetchData = (value, callback) => {
    if(localStorage.getItem('without_server') === 'true') {
      callback(defaultSelectData)
    }
  };

  handleSelectChange = (value) => {
    this.setState({selectValue: value});
    this.fetchData(value, data => this.setState({selectData: data}));
    if(value === '') {
      this.setState({selectData: []})
    }
  };

  onSetResult(result) {
    this.setState({
      title: result.groupInfo.title,
      isActive: result.groupInfo.isActive,
      size: result.groupInfo.size,
      moneyPerUser: result.groupInfo.moneyPerUser,
      groupType: result.groupInfo.groupType,
      tags: result.groupInfo.tags,
      description: result.groupInfo.description,
      isPrivate: result.groupInfo.isPrivate,
      members: result.members,
      teacher: result.teacher,
      isInGroup: this.state.userData ? Boolean(result.members.find(item => item.userId === this.state.userData.UserId)) : false
    })
  }

  inviteMember() {
    if(localStorage.getItem('without_server') === 'true') {
      message.success('Приглашение отправлено')
    }
    else {
      fetch(`${config.API_LOCAL_URL}/group/${this.state.id}/member/invite/${this.state.selectValue}`, {
        headers: {
          'Method': 'POST',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
        .catch(error => error)
    }

    setTimeout(() => this.setState({selectValue: ''}), 0);
  }

  // leaveGroup() {
  //   this.setState({isInGroup: false})
  // }
  //
  // enterGroup() {
  //   this.setState({isInGroup: true})
  // }

  render() {
    return (
      this.state.isActive ?
        (<div>
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
                      <Link key={item} to="#">{item}</Link>
                    )}
                  </Row>
                  <Row type='flex' justify='space-between' style={{marginBottom: 8}}>
                    <Col>Формат</Col>
                    <Col>{getGroupType(this.state.groupType)}</Col>
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
                <Row>
                  {this.state.isInGroup ? getMemberRole(
                    this.state.members.find(item =>
                      item.userId === this.state.userData.UserId).memberRole) === 'Создатель' ?
                      (<Row className='md-center-container'>
                        <Dropdown
                          overlay={this.inviteMenu(this.state.selectData)}
                          onVisibleChange={this.handleVisibleChange}
                          visible={this.state.inviteVisible}
                          trigger={['click']}
                        >
                          <Button
                            size='large'
                            style={{width: 280, marginLeft: -16}}
                            type='primary'
                          >
                            Пригласить
                          </Button>
                        </Dropdown>
                      </Row>) : null
                    : null
                  }
                </Row>
              </Col>
              <Col xs={{span: 24}} md={{span: 13, offset: 1}} lg={{span: 16, offset: 1}}>
                <Row className='md-center-container' style={{textAlign: 'right', marginTop: 8}}>
                  { this.state.isInGroup ?
                    (<Button onClick={() => this.props.leaveGroup(this.state.id, this.state.userData.UserId)}>Покинуть группу</Button>)
                    : (<Button type='primary' onClick={() => this.props.enterGroup(this.state.id)}>Вступить в группу</Button>)
                  }
                </Row>
                <Row>
                  <Row style={{marginTop: 42}}>
                    <Col><h3 style={{fontSize: 18}}>Описание</h3></Col>
                  </Row>
                  <Row style={{marginBottom: 40}}>
                    <p>
                      {this.state.description}
                    </p>
                  </Row>
                </Row>
                <Row style={{width: '100%'}}>
                  <Chat isInGroup={this.state.isInGroup}/>
                </Row>
              </Col>
            </Col>
          </div>
        ) :
        (<Row type='flex' justify='center' style={{marginTop: 20}}>Данной группы не существует</Row>)
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
  currentGroupData: makeSelectGroupData()
});

function mapDispatchToProps(dispatch) {
  return {
    getCurrentGroupData: (groupId) => dispatch(getGroupData(groupId)),
    enterGroup: (groupId) => dispatch(enterGroup(groupId)),
    leaveGroup: (groupId, memberId) => dispatch(leaveGroup(groupId, memberId))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'groupPage', reducer });
const withSaga = injectSaga({ key: 'groupPage', saga });

export default withRouter(compose(
  withReducer,
  withSaga,
  withConnect,
)(GroupPage));
