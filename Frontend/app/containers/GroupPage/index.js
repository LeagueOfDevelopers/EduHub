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
import {
  enterGroup,
  leaveGroup,
  editGroupTitle,
  editGroupDescription,
  editGroupTags,
  editGroupSize,
  editGroupPrice,
  editPrivacy,
  editGroupType,
  getCurrentPlan
} from "./actions";
import { makeSelectNeedUpdate, makeSelectPlan } from "./selectors";
import {Link} from "react-router-dom";
import config from "../../config";
import {getGroupType, parseJwt, getMemberRole} from "../../globalJS";
import {Col, Row, Button, message, Input, Select, InputNumber, Switch} from 'antd';
import MemberList from '../../components/MembersList/Loadable';
import Chat from '../../components/Chat/Loadable';
import InviteMemberSelect from '../../components/InviteMemberSelect/Loadable';
import SigningInForm from "../../containers/SigningInForm/index";
import SuggestPlanForm from '../../components/SuggestPlanForm';
import ReviewModal from '../../components/ReviewModal';

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
          size: 0,
          groupType: '',
          memberAmount: 0,
          curriculum: null
        },
        members: []
      },
      userData: localStorage.getItem('token') ? parseJwt(localStorage.getItem('token')) : null,
      isInGroup: false,
      isCreator: false,
      isTeacher: false,
      signInVisible: false,
      reviewVisible: false,
      isEditing: false,
      titleInput: '',
      descInput: '',
      tagsInput: [],
      sizeInput: '',
      priceInput: '',
      groupTypeInput: '',
      privateInput: null
    };

    this.onSetResult = this.onSetResult.bind(this);
    this.getCurrentGroup = this.getCurrentGroup.bind(this);
    this.onSignInClick = this.onSignInClick.bind(this);
    this.handleSignInCancel = this.handleSignInCancel.bind(this);
    this.onReviewClick = this.onReviewClick.bind(this);
    this.handleReviewCancel = this.handleReviewCancel.bind(this);
    this.changeGroupData = this.changeGroupData.bind(this);
    this.onChangeTitleHandle = this.onChangeTitleHandle.bind(this);
    this.onChangeDescriptionHandle = this.onChangeDescriptionHandle.bind(this);
    this.onChangePriceHandle = this.onChangePriceHandle.bind(this);
    this.onChangeSizeHandle = this.onChangeSizeHandle.bind(this);
    this.onChangeTagsHandle = this.onChangeTagsHandle.bind(this);
    this.onHandleGroupTypeChange = this.onHandleGroupTypeChange.bind(this);
    this.onHandlePrivateChange = this.onHandlePrivateChange.bind(this);
    this.cancelChanges = this.cancelChanges.bind(this);
  }

  onSignInClick = () => {
    this.setState({signInVisible: true})
  };

  handleSignInCancel = () => {
    this.setState({signInVisible: false})
  };

  onReviewClick = () => {
    this.setState({reviewVisible: true})
  };

  handleReviewCancel = () => {
    this.setState({reviewVisible: false})
  };

  getCurrentGroup = () => {
    if(localStorage.getItem('without_server') !== 'true') {
      fetch(`${config.API_BASE_URL}/group/${this.state.id}`, {
        headers: {
          'Content-Type': 'application/json-patch+json'
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
    this.getCurrentGroup();

    if(this.props.match.params.review === 'review') {
      setTimeout(this.onReviewClick, 1000);
    }
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevProps.needUpdate !== this.props.needUpdate) {
      this.getCurrentGroup();
    }
  }

  onSetResult(result) {
    this.setState({
      groupData: {
        groupInfo: result.groupInfo,
        members: result.members
      },
      titleInput: result.groupInfo.title,
      descInput: result.groupInfo.description,
      sizeInput: result.groupInfo.size,
      priceInput: result.groupInfo.cost,
      tagsInput: result.groupInfo.tags,
      groupTypeInput: getGroupType(result.groupInfo.groupType),
      privateInput: result.groupInfo.isPrivate,
      isInGroup: this.state.userData ?
        Boolean(result.members.find(item => item.userId === this.state.userData.UserId)) : false
      });
    this.setState({
      isCreator: this.state.isInGroup ? Boolean(result.members.find(item =>
          item.userId === this.state.userData.UserId).role === 2) : false,
      isTeacher: this.state.isInGroup ? Boolean(result.members.find(item =>
        item.userId === this.state.userData.UserId).role === 3) : false
    });
    this.state.groupData.groupInfo.curriculum ?
      setTimeout(() => this.props.getCurrentPlan(this.state.groupData.groupInfo.curriculum), 0)
      : null
  }

  onChangeTitleHandle = (e) => {
    this.setState({titleInput: e.target.value})
  };

  onChangeDescriptionHandle = (e) => {
    this.setState({descInput: e.target.value})
  };

  onChangeSizeHandle = (e) => {
    this.setState({sizeInput: e})
  };

  onChangePriceHandle = (e) => {
    this.setState({priceInput: e})
  };

  onChangeTagsHandle = (e) => {
    this.setState({tagsInput: e})
  };

  onHandleGroupTypeChange = (e) => {
    this.setState({groupTypeInput: e})
  };

  onHandlePrivateChange = (e) => {
    this.setState({privateInput: e})
  };

  cancelChanges = () => {
    this.setState({
      isEditing: false,
      titleInput: this.state.groupData.groupInfo.title,
      descInput: this.state.groupData.groupInfo.description,
      sizeInput: this.state.groupData.groupInfo.size,
      priceInput: this.state.groupData.groupInfo.cost,
      tagsInput: this.state.groupData.groupInfo.tags
    })
  };

  changeGroupData = () => {
    if(this.state.titleInput !== this.state.groupData.groupInfo.title) {
      this.props.editGroupTitle(this.state.id, this.state.titleInput);
    }
    if(this.state.descInput !== this.state.groupData.groupInfo.description) {
      this.props.editGroupDescription(this.state.id, this.state.descInput);
    }
    if(this.state.sizeInput !== this.state.groupData.groupInfo.size) {
      this.props.editGroupSize(this.state.id, this.state.sizeInput);
    }
    if(this.state.priceInput !== this.state.groupData.groupInfo.cost) {
      this.props.editGroupPrice(this.state.id, this.state.priceInput);
    }
    if(this.state.groupTypeInput !== getGroupType(this.state.groupData.groupInfo.groupType)) {
      this.props.editGroupType(this.state.id, this.state.groupTypeInput);
    }
    if(this.state.privateInput !== this.state.groupData.groupInfo.isPrivate) {
      this.props.editPrivacy(this.state.id, this.state.privateInput);
    }
    if(this.state.tagsInput.length !== this.state.groupData.groupInfo.tags.length || this.state.tagsInput.filter((item, i) =>
        item !== this.state.groupData.groupInfo.tags[i]
      ).length !== 0) {
      this.props.editGroupTags(this.state.id, this.state.tagsInput)
    }
    this.setState({isEditing: false});
  };

  render() {
    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40, marginBottom: 160, fontSize: 16}}>
          <Col className='md-center-container' xs={{span: 24}} md={{span: 10}} lg={{span: 7}}>
            <Row className='main-group-info'>
              <Row style={{marginBottom: 26}}>
                <Col span={24}>
                  <h3 className='word-break' style={{margin: 0, fontSize: 22}}>
                    {this.state.isEditing ?
                      <Input onChange={this.onChangeTitleHandle} value={this.state.titleInput}/>
                      : this.state.groupData.groupInfo.title}
                  </h3>
                </Col>
                { Boolean(this.state.groupData.members.find(item =>
                  item.role == 3)) ?
                  (<span style={{color: 'rgba(0,0,0,0.6)'}}>Преподаватель найден</span>)
                  : (<span style={{color: 'rgba(0,0,0,0.6)'}}>Идет поиск преподавателя</span>)
                }
              </Row>
              <Row gutter={6} type='flex' justify='start' align='middle' style={{marginBottom: 8}}>
                {this.state.isEditing ?
                  <Select onChange={this.onChangeTagsHandle} defaultActiveFirstOption={false} value={this.state.tagsInput} mode="tags" style={{width: '100%'}}>
                    <Select.Option value="html">html</Select.Option>
                    <Select.Option value="css">css</Select.Option>
                    <Select.Option value="js">js</Select.Option>
                    <Select.Option value="c#">c#</Select.Option>
                  </Select>
                  : this.state.groupData.groupInfo.tags.map((item) =>
                  <Link key={item} to="#">{item}</Link>
                )}
              </Row>
              <Row type='flex' justify='space-between' align='middle' style={{marginBottom: 8}}>
                <Col>Формат</Col>
                <Col>
                  {this.state.isEditing ?
                    <Select onChange={this.onHandleGroupTypeChange} defaultActiveFirstOption={false} value={this.state.groupTypeInput} style={{minWidth: 114}}>
                      <Select.Option value='Lecture'>Лекция</Select.Option>
                      <Select.Option value='MasterClass'>Мастер-класс</Select.Option>
                      <Select.Option value='Seminar'>Семинар</Select.Option>
                    </Select>
                    : getGroupType(this.state.groupData.groupInfo.groupType)
                  }
                </Col>
              </Row>
              <Row type='flex' justify='space-between' align='middle' style={{marginBottom: 8}}>
                <Col>Стоимость</Col>
                <Col>
                  {this.state.isEditing ?
                    <InputNumber min={0} value={this.state.priceInput} onChange={this.onChangePriceHandle}/>
                    : this.state.groupData.groupInfo.cost} руб.
                </Col>
              </Row>
              {this.state.isEditing ?
                <Row type='flex' align='middle' style={{marginBottom: 12}}>
                  <Col span={16}>
                    <label htmlFor="privacy">Приватная группа</label>
                  </Col>
                  <Col span={8} style={{textAlign: 'right'}}>
                    <Switch value={this.state.privateInput} id='privacy' onChange={this.onHandlePrivateChange}/>
                  </Col>
                </Row>
                : this.state.groupData.groupInfo.isPrivate ?
                  (<Row style={{marginBottom: 12}}>
                    <Col>Эта группа является приватной</Col>
                  </Row>)
                  : (<Row style={{marginBottom: 12}}>
                    <Col>Эта группа не является приватной</Col>
                  </Row>)
              }
              {this.state.isEditing ?
                <Row type='flex' align='middle' style={{marginBottom: 12}}>
                  <Col span={10}>
                    <label htmlFor="size">Участников</label>
                  </Col>
                  <Col span={14} style={{textAlign: 'right'}}>
                    <InputNumber min={0} id='size' value={this.state.sizeInput} onChange={this.onChangeSizeHandle} style={{width: 64}}/>
                  </Col>
                </Row>
                : null
              }
            </Row>
            <Row style={{width: '100%', marginBottom: 20}}>
              <MemberList
                groupId={this.state.id}
                members={this.state.groupData.members}
                memberAmount={this.state.groupData.groupInfo.memberAmount}
                size={this.state.groupData.groupInfo.size}
                isCreator={this.state.isCreator}
              />
            </Row>
            {this.state.isCreator ?
              (<Row style={{width: '100%'}} className='md-center-container'>
                <InviteMemberSelect groupId={this.state.id}/>
              </Row>) : null
            }
            {this.state.isCreator && !this.state.isEditing ?
              <Row>
                <Button type='dashed' className='md-center-container md-offset-16px' onClick={() => this.setState({isEditing: true})} style={{width: 280, marginTop: 12}}>Редактировать</Button>
              </Row>
              : this.state.isEditing ?
                <Row>
                  <Col span={24} className='md-center-container md-offset-16px'>
                    <Button type='primary' onClick={this.changeGroupData} style={{width: 280, marginTop: 22}}>Подтвердить</Button>
                  </Col>
                  <Col span={24} className='md-center-container md-offset-16px'>
                    <Button type='danger' onClick={this.cancelChanges} style={{width: 280, marginTop: 10}}>Отмена</Button>
                  </Col>
                </Row>
                : null
            }
          </Col>
          <Col xs={{span: 24}} md={{span: 12, offset: 2}} lg={{span: 15, offset: 2}} xl={{span: 16, offset: 1}}>
            <Row style={{textAlign: 'left', marginTop: 8}}>
              <Col xs={{span: 24}} lg={{span: 17}}>
                <SuggestPlanForm
                  members={this.state.groupData.members}
                  groupId={this.state.id}
                  curriculum={this.state.groupData.groupInfo.curriculum}
                  isTeacher={this.state.isTeacher}
                  currentUserData={this.state.userData}
                  currentPlan={this.props.currentPlan}
                />
              </Col>
              <Col xs={{span: 24}} lg={{span: 7}} style={{textAlign: 'right'}}>
                {this.state.groupData.groupInfo.memberAmount < this.state.groupData.groupInfo.size ?
                  this.state.isInGroup ?
                    (<Row className='lg-center-container-item'>
                        <Button className='group-btn' onClick={() => {
                          this.props.leaveGroup(this.state.id, this.state.userData.UserId, this.state.isTeacher ? 'Teacher' : 'Member')
                        }}
                        >
                          Покинуть группу
                        </Button>
                      </Row>
                    )
                    : (<Row className='lg-center-container-item'>
                        <Button type='primary' className='group-btn' onClick={() => {
                          if(this.state.userData) {
                            this.props.enterGroup(this.state.id);
                          }
                          else {
                            this.onSignInClick()
                          }
                        }}
                        >
                          Вступить в группу
                        </Button>
                      </Row>
                    )
                  : null
                }
              </Col>
            </Row>
            <Row>
              <Row style={{marginTop: 42}}>
                <Col><h3 style={{fontSize: 18}}>Описание</h3></Col>
              </Row>
              <Row style={{marginBottom: 40}}>
                <p className='word-break'>
                  {this.state.isEditing ?
                    <Input.TextArea onChange={this.onChangeDescriptionHandle} defaultValue={this.state.descInput} autosize/>
                    : this.state.groupData.groupInfo.description}
                </p>
              </Row>
            </Row>
            <Row style={{width: '100%'}}>
              <Chat isInGroup={this.state.isInGroup}/>
            </Row>
          </Col>
        </Col>
        <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleSignInCancel}/>
        <ReviewModal courseTitle={this.state.groupData.groupInfo.title} visible={this.state.reviewVisible} handleCancel={this.handleReviewCancel}/>
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
  needUpdate: makeSelectNeedUpdate(),
  currentPlan: makeSelectPlan()
});

function mapDispatchToProps(dispatch) {
  return {
    enterGroup: (groupId) => dispatch(enterGroup(groupId)),
    leaveGroup: (groupId, memberId, role) => dispatch(leaveGroup(groupId, memberId, role)),
    editGroupTitle: (id, title) => dispatch(editGroupTitle(id, title)),
    editGroupDescription: (id, description) => dispatch(editGroupDescription(id, description)),
    editGroupTags: (id, tags) => dispatch(editGroupTags(id, tags)),
    editGroupSize: (id, size) => dispatch(editGroupSize(id, size)),
    editGroupPrice: (id, price) => dispatch(editGroupPrice(id, price)),
    editPrivacy: (id, isPrivate) => dispatch(editPrivacy(id, isPrivate)),
    editGroupType: (id, type) => dispatch(editGroupType(id, type)),
    getCurrentPlan: (filename) => dispatch(getCurrentPlan(filename))
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
