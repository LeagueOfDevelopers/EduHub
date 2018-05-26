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
  getCurrentChat,
  getCurrentPlan,
  getTags,
  clearChat
} from "./actions";
import { makeSelectNeedUpdate, makeSelectChat, makeSelectTags } from "./selectors";
import {Link} from "react-router-dom";
import config from "../../config";
import {getGroupType, parseJwt, getMemberRole, connectSockets, getCourseStatus} from "../../globalJS";
import {Col, Row, Button, message, Input, Select, InputNumber, Switch, Form} from 'antd';
import MemberList from '../../components/MembersList/Loadable';
import Chat from '../../components/Chat/Loadable';
import InviteMemberSelect from '../../components/InviteMemberSelect/Loadable';
import SigningInForm from "../../containers/SigningInForm/index";
import SuggestPlanForm from '../../components/SuggestPlanForm';
import ReviewModal from '../../components/ReviewModal';
import EnterGroupBtn from '../../components/EnterGroupBtn';

const FormItem = Form.Item;

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
          isPrivate: false,
          title: '',
          description: '',
          isActive: true,
          tags: [],
          cost: null,
          size: 0,
          groupType: '',
          memberAmount: 0,
          courseStatus: 0,
          curriculum: null
        },
        members: [],
        reviews: []
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
      privateInput: false
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
    this.onHandleSearch = this.onHandleSearch.bind(this);
    this.validateTagsInput = this.validateTagsInput.bind(this);
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
          'Content-Type': 'application/json-patch+json',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
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
  }

  componentWillUnmount() {
    this.props.clearChat();
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevProps.needUpdate && !this.props.needUpdate) {
      this.componentDidMount()
    }
  }

  onSetResult(result) {
    this.setState({
      groupData: {
        groupInfo: result.groupInfo,
        members: result.members,
        reviews: result.reviews
      },
      titleInput: result.groupInfo.title,
      descInput: result.groupInfo.description,
      sizeInput: result.groupInfo.size,
      priceInput: result.groupInfo.cost,
      tagsInput: result.groupInfo.tags,
      groupTypeInput: getGroupType(result.groupInfo.groupType),
      privateInput: result.groupInfo.isPrivate,
      isInGroup: this.state.userData ?
        Boolean(result.members.find(item => item.userId == this.state.userData.UserId)) : false
      });
    this.setState({
      isCreator: this.state.isInGroup ? Boolean(result.members.find(item =>
          item.userId == this.state.userData.UserId).role === 2) : false,
      isTeacher: this.state.isInGroup ? Boolean(result.members.find(item =>
        item.userId == this.state.userData.UserId).role === 3) : false
    });
    localStorage.getItem('token') && this.state.isInGroup ? this.props.getCurrentChat(this.state.id) : null;

    if(this.state.groupData.groupInfo.courseStatus === 3 && !this.state.isTeacher && this.state.isInGroup && !this.state.groupData.reviews.filter(item => item.fromUser == this.state.userData.UserId).length) {
      setTimeout(this.onReviewClick, 1000);
    }
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

  onHandleSearch = (e) => {
    this.props.getTags(e);
  };

  validateTagsInput = (rule, value, callback) => {
    if (value.length < 3) {
      callback('Должно быть не менее 3 тегов!')
    }
    else if (value.length > 10) {
      callback('Должно быть не более 10 тегов!')
    }
    else if (value.filter(item => item.length > 16).length !== 0) {
      callback('В теге не может быть более 16 символов!')
    }
    else {
      callback()
    }
  };

  cancelChanges = () => {
    this.setState({
      isEditing: false,
      titleInput: this.state.groupData.groupInfo.title,
      descInput: this.state.groupData.groupInfo.description,
      sizeInput: this.state.groupData.groupInfo.size,
      priceInput: this.state.groupData.groupInfo.cost,
      tagsInput: this.state.groupData.groupInfo.tags,
      privateInput: this.state.groupData.groupInfo.isPrivate
    })
  };

  changeGroupData = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
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
      }
    });
  };

  render() {
    const {getFieldDecorator} = this.props.form;
    return (
      <div>
        <Form className='group-form' onSubmit={this.changeGroupData}>
          <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}} style={{marginTop: 40, marginBottom: 40, fontSize: 16}}>
            <Col className='md-center-container' xs={{span: 24}} md={{span: 10}} lg={{span: 7}}>
              <Row className='main-group-info'>
                <Row style={{marginBottom: 26}}>
                  <Col span={24}>
                    <h3 className='word-break' style={{margin: 0, fontSize: 22}}>
                      {this.state.isEditing ?
                        <FormItem style={{width: '100%', marginBottom: 0}}>
                          {getFieldDecorator('title', {
                            rules: [
                              {required: true, message: 'Пожалуйста, введите название группы!'},
                              {min: 3, message: 'Название должно содержать не менее 3 символов!'},
                              {max: 70, message: 'Название должно содержать не более 70 символов!'}
                            ],
                            initialValue: this.state.titleInput
                          })(
                            <Input onChange={this.onChangeTitleHandle}/>)
                          }
                        </FormItem>
                        : this.state.groupData.groupInfo.title}
                    </h3>
                  </Col>
                  {
                    getCourseStatus(this.state.groupData.groupInfo.courseStatus)
                  }
                </Row>
                <Row gutter={6} type='flex' justify='space-between' align='middle' style={{marginBottom: 8}}>
                  <Col>Тэги</Col>
                  <Col>
                    {this.state.isEditing ?
                      <FormItem style={{width: '100%', marginBottom: 0}}>
                        {getFieldDecorator('tags', {
                          rules: [
                            {required: true, message: 'Пожалуйста, введите изучаемые технологии!'},
                            {validator: this.validateTagsInput}
                          ],
                          initialValue: this.state.tagsInput
                        })(
                          <Select onChange={this.onChangeTagsHandle} style={{width: '100%'}} onSearch={this.onHandleSearch} mode="tags" placeholder="Введите, что хотите изучить" notFoundContent={null}>
                            {this.props.tags.length && this.props.tags.length !== 0 ?
                              this.props.tags.map((item, index) =>
                                <Option key={item.tag}>{item.tag}</Option>
                              ) : null}
                          </Select>)
                        }
                      </FormItem>
                      : this.state.groupData.groupInfo.tags.map((item) =>
                        <Link key={item} to={`/groups?tags=${item.replace('#', '*')}`}>{item} </Link>
                      )}
                  </Col>
                </Row>
                <Row type='flex' justify='space-between' align='middle' style={{marginBottom: 8}}>
                  <Col>Формат</Col>
                  <Col>
                    {this.state.isEditing ?
                      <FormItem style={{width: '100%', marginBottom: 0}}>
                        {getFieldDecorator('type', {
                          rules: [{required: true, message: 'Пожалуйста, выберите формат обучения!'}],
                          initialValue: this.state.groupTypeInput
                        })(
                          <Select onChange={this.onHandleGroupTypeChange} defaultActiveFirstOption={false} style={{minWidth: 114}} placeholder="Выберите формат">
                            <Select.Option value="Lecture">Лекция</Select.Option>
                            <Select.Option value="MasterClass">Мастер-класс</Select.Option>
                            <Select.Option value="Seminar">Семинар</Select.Option>
                          </Select>)
                        }
                      </FormItem>
                      : getGroupType(this.state.groupData.groupInfo.groupType)
                    }
                  </Col>
                </Row>
                <Row type='flex' justify='space-between' align='middle' style={{marginBottom: 8}}>
                  <Col span={10}>Стоимость</Col>
                  <Col span={14} title={`${this.state.groupData.groupInfo.cost} руб.`} style={{textAlign: 'right', textOverflow: 'ellipsis', whiteSpace: 'nowrap', overflow: 'hidden'}}>
                    {this.state.isEditing ?
                      <FormItem style={{width: '100%', marginBottom: 0}}>
                        {getFieldDecorator('price', {
                          rules: [
                            {required: true, message: 'Пожалуйста, введите стоимость занятия!'}
                          ],
                          initialValue: this.state.priceInput
                        })(
                          <InputNumber min={1} onChange={this.onChangePriceHandle}/>)
                        }
                      </FormItem>
                      : `${this.state.groupData.groupInfo.cost} руб.`
                    }
                  </Col>
                </Row>
                {this.state.isEditing ?
                  <Row type='flex' align='middle' style={{marginBottom: 12}}>
                    <Col span={16}>
                      Приватная группа
                    </Col>
                    <Col span={8} style={{textAlign: 'right'}}>
                      <Switch checked={this.state.privateInput} id='privacy' onChange={this.onHandlePrivateChange}/>
                    </Col>
                  </Row>
                  : this.state.groupData.groupInfo.isPrivate ?
                    (<Row style={{marginBottom: 12}}>
                      <Col>Эта группа является приватной</Col>
                    </Row>)
                    : null
                }
                {this.state.isEditing ?
                  <Row type='flex' align='middle' style={{marginBottom: 12}}>
                    <Col span={10}>
                      Участников
                    </Col>
                    <Col span={14} style={{textAlign: 'right'}}>
                      <FormItem style={{width: '100%', marginBottom: 0}}>
                        {getFieldDecorator('size', {
                          rules: [{required: true, message: 'Пожалуйста, введите количество человек!'}],
                          initialValue: this.state.sizeInput
                        })(
                          <InputNumber id='size' min={1} max={200} onChange={this.onChangeSizeHandle} style={{width: 64}}/>)
                        }
                      </FormItem>
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
              {this.state.isCreator && !this.state.isEditing && this.state.groupData.groupInfo.courseStatus !== 2 && this.state.groupData.groupInfo.courseStatus !== 3 ?
                (<Row style={{width: 'calc(100% + 32px)'}} className='md-center-container'>
                  <InviteMemberSelect groupId={this.state.id} memberAmount={this.state.groupData.groupInfo.memberAmount} size={this.state.groupData.groupInfo.size} teacher={this.state.groupData.members.filter(item => item.role === 3)[0]}/>
                </Row>) : null
              }
              {this.state.isCreator && !this.state.isEditing && this.state.groupData.groupInfo.courseStatus !== 2 && this.state.groupData.groupInfo.courseStatus !== 3 ?
                <Row style={{width: 'calc(100% + 32px)'}}>
                  <Button type='dashed' className='md-center-container md-offset-16px' onClick={() => this.setState({isEditing: true})} style={{width: '100%', marginBottom: 12}}>Редактировать</Button>
                </Row>
                : this.state.isEditing ?
                  <Row style={{width: 'calc(100% + 32px)'}}>
                    <Col span={24} className='md-center-container md-offset-16px'>
                      <Button type='primary' htmlType='submit' style={{width: '100%'}}>Подтвердить</Button>
                    </Col>
                    <Col span={24} className='md-center-container md-offset-16px'>
                      <Button type='danger' onClick={this.cancelChanges} style={{width: '100%', marginTop: 10}}>Отмена</Button>
                    </Col>
                  </Row>
                  : null
              }
            </Col>
            <Col xs={{span: 24}} md={{span: 12, offset: 2}} lg={{span: 15, offset: 2}} xl={{span: 15, offset: 2}}>
              <Row>
                <Row style={{marginTop: 4}}>
                  <Col span={10}><span style={{fontSize: 18, fontWeight: 600}}>Описание</span></Col>
                  <Col span={14} style={{textAlign: 'right'}}>
                    <EnterGroupBtn
                      memberAmount={this.state.groupData.groupInfo.memberAmount}
                      size={this.state.groupData.groupInfo.size}
                      isInGroup={this.state.isInGroup}
                      isTeacher={this.state.isTeacher}
                      userData={this.state.userData}
                      groupId={this.state.id}
                      onSignInClick={this.onSignInClick}
                      members={this.state.groupData.members}
                      courseStatus={this.state.groupData.groupInfo.courseStatus}
                    />
                  </Col>
                </Row>
                <Row style={{marginBottom: 22, marginTop: 4}}>
                  <p className='word-break'>
                    {this.state.isEditing ?
                      <FormItem style={{width: '100%', marginBottom: 0}}>
                        {getFieldDecorator('desc', {
                          rules: [
                            {required: true, message: 'Пожалуйста, введите описание!'},
                            {min: 20, message: 'Должно быть не менее 20 символов!'},
                            {max: 3000, message: 'Должно быть не более 3000 символов!'}
                          ],
                          initialValue: this.state.descInput
                        })(
                          <Input.TextArea onChange={this.onChangeDescriptionHandle} autosize/>)
                        }
                      </FormItem>
                      : this.state.groupData.groupInfo.description}
                  </p>
                </Row>
                {
                  (this.state.groupData.groupInfo.curriculum || this.state.isTeacher) && this.state.isInGroup ?
                    <Row style={{textAlign: 'left', marginTop: 8, marginBottom: 28}}>
                      <Col style={{marginBottom: 8}}><span style={{fontSize: 18, fontWeight: 600}}>Учебный план</span></Col>
                      <Col xs={{span: 24}}>
                        <SuggestPlanForm
                          members={this.state.groupData.members}
                          groupId={this.state.id}
                          curriculum={this.state.groupData.groupInfo.curriculum}
                          isTeacher={this.state.isTeacher}
                          currentUserData={this.state.userData}
                          currentPlan={this.state.groupData.groupInfo.curriculum}
                          courseStatus={this.state.groupData.groupInfo.courseStatus}
                          isInGroup={this.state.isInGroup}
                        />
                      </Col>
                    </Row>
                    : null
                }
              </Row>
              <Row style={{width: '100%', paddingBottom: 40}}>
                {
                  <Chat chat={this.state.isInGroup ? this.props.currentChat : []} groupId={this.state.id} isInGroup={this.state.isInGroup}/>
                }
              </Row>
            </Col>
          </Col>
        </Form>
        <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleSignInCancel}/>
        <ReviewModal courseTitle={this.state.groupData.groupInfo.title} groupId={this.state.id} visible={this.state.reviewVisible} handleCancel={this.handleReviewCancel}/>
      </div>
    );
  }
}

GroupPage.propTypes = {
  title: PropTypes.string,
  isActive: PropTypes.bool,
  tags: PropTypes.oneOfType([
    PropTypes.array,
    PropTypes.object
  ]),
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
  currentChat: makeSelectChat(),
  tags: makeSelectTags()
});

function mapDispatchToProps(dispatch) {
  return {
    enterGroup: (groupId, role) => dispatch(enterGroup(groupId, role)),
    leaveGroup: (groupId, memberId, role) => dispatch(leaveGroup(groupId, memberId, role)),
    editGroupTitle: (id, title) => dispatch(editGroupTitle(id, title)),
    editGroupDescription: (id, description) => dispatch(editGroupDescription(id, description)),
    editGroupTags: (id, tags) => dispatch(editGroupTags(id, tags)),
    editGroupSize: (id, size) => dispatch(editGroupSize(id, size)),
    editGroupPrice: (id, price) => dispatch(editGroupPrice(id, price)),
    editPrivacy: (id, isPrivate) => dispatch(editPrivacy(id, isPrivate)),
    editGroupType: (id, type) => dispatch(editGroupType(id, type)),
    getCurrentChat: (groupId) => dispatch(getCurrentChat(groupId)),
    getCurrentPlan: (plan) => dispatch(getCurrentPlan(plan)),
    getTags: (tag) => dispatch(getTags(tag)),
    clearChat: () => dispatch(clearChat())
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'groupPage', reducer });
const withSaga = injectSaga({ key: 'groupPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(Form.create()(GroupPage));
