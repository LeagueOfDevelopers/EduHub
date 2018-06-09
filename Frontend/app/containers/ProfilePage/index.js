/**
 *
 * ProfilePage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import moment from 'moment';
import {makeSelectUserGroups, makeSelectNeedUpdate, makeSelectTags} from './selectors';
import {
  getCurrentUserGroups,
  editUsername,
  editAboutUserInfo,
  editGender,
  editBirthYear,
  editContacts,
  makeTeacher,
  makeNotTeacher,
  editProfile,
  editSkills,
  getTags
} from "./actions";
import reducer from './reducer';
import saga from './saga';
import {parseJwt, getGender, getGenderType, getGroupCardWidth} from "../../globalJS";
import config from '../../config';
import {Link} from "react-router-dom";
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";
import MakeReportModal from '../../components/MakeReportModal';
import {Card, Col, Row, Avatar, Tabs, Input, DatePicker, Select, Button, Icon, Upload, Form} from 'antd';
const FormItem = Form.Item;
const TabPane = Tabs.TabPane;

const defaultUserData = {
  userProfile: {
    name: 'Имя пользователя',
    tags: ['js', 'c#'],
    sex: 'Мужской',
    years: 19,
    experience: 3,
    description:
    'Краткая инфа о себе. Краткая инфа о себе. Краткая инфа о себе.\n' +
    '                  Краткая инфа о себе.',
    links: ['LinkedIn', 'Vk']
  }
};

const defaultMyGroups = [
  {
    groupInfo: {
      id: 1,
      title: 'cdcvvdsc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  },
  {
    groupInfo: {
      id: 3,
      title: 'dscsdc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  },
  {
    groupInfo: {
      id: 1,
      title: 'cdcvvdsc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  },
  {
    groupInfo: {
      id: 3,
      title: 'dscsdc',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    }
  }
];

const defaultTeacherProfile = {
  reviews: [
    {
      fromUser: 'DJLSFFSKJ',
      fromGroup: 'wqe1wqedsadsa',
      title: "stringdskjn ndknskn",
      text: "stringstringstringstringstring stringstringstringstringstring        text: \"stringstringstringstringstring stringstringstringstringstring \",\n",
      date: "2018-04-15T13:15:39.510Z"
    },
    {
      fromUser: 'qweqrr',
      fromGroup: 'wqe1wqedsadsa',
      title: "string",
      text: "string",
      date: "2018-04-15T13:15:39.510Z"
    },
    {
      fromUser: 'qweqrr',
      fromGroup: 'wqe1wqedsadsa',
      title: "string",
      text: "string",
      date: "2018-04-15T13:15:39.510Z"
    },
    {
      fromUser: 'qweqrr',
      fromGroup: 'wqe1wqedsadsa',
      title: "string",
      text: "string",
      date: "2018-04-15T13:15:39.510Z"
    },
    {
      fromUser: 'qweqrr',
      fromGroup: 'wqe1wqedsadsa',
      title: "string",
      text: "string",
      date: "2018-04-15T13:15:39.510Z"
    },
    {
      fromUser: 'qweqrr',
      fromGroup: 'wqe1wqedsadsa',
      title: "string",
      text: "string",
      date: "2018-04-15T13:15:39.510Z"
    },
    {
      fromUser: 'qweqrr',
      fromGroup: 'wqe1wqedsadsa',
      title: "string",
      text: "string",
      date: "2018-04-15T13:15:39.510Z"
    },
  ]
};

export class ProfilePage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      id: this.props.match.params.id,
      userProfile: {
        name: '',
        email: '',
        avatarLink: '',
        gender: '',
        birthYear: '',
        aboutUser: '',
        contacts: []
      },
      teacherProfile: null,
      isEditing: false,
      nameInput: '',
      genderInput: 'Unknown',
      birthYearInput: '',
      skillsInput: [],
      aboutInput: '',
      imageUrl: null,
      avatarLoading: false,
      contactsInputs: [],
      userData: localStorage.getItem('token') ? parseJwt(localStorage.getItem('token')) : null,
      isCurrentUser: false,
      reportVisible: false,
      groupCardWidth: '100%',
      currentDate: new Date().toISOString()
    };

    this.onSetResult = this.onSetResult.bind(this);
    this.onReportClick = this.onReportClick.bind(this);
    this.handleReportCancel = this.handleReportCancel.bind(this);
    this.getCurrentUser = this.getCurrentUser.bind(this);
    this.onChangeNameHandle = this.onChangeNameHandle.bind(this);
    this.onChangeGenderHandle = this.onChangeGenderHandle.bind(this);
    this.onChangeBirthYearHandle = this.onChangeBirthYearHandle.bind(this);
    this.onChangeAboutHandle = this.onChangeAboutHandle.bind(this);
    this.changeProfileData = this.changeProfileData.bind(this);
    this.addContact = this.addContact.bind(this);
    this.removeContact = this.removeContact.bind(this);
    this.cancelChanges = this.cancelChanges.bind(this);
    this.handleAvatarLinkChange = this.handleAvatarLinkChange.bind(this);
    this.onHandleSearch = this.onHandleSearch.bind(this);
  }

  onReportClick = () => {
    this.setState({reportVisible: true})
  };

  handleReportCancel = () => {
    this.setState({reportVisible: false})
  };

  onHandleSearch = (e) => {
    this.props.getTags(e);
  };

  componentDidMount() {
    this.setState({groupCardWidth: getGroupCardWidth()});
    if(localStorage.getItem('without_server') !== 'true') {
      this.getCurrentUser(this.props.match.params.id);
      this.props.getCurrentUserGroups(this.props.match.params.id);
    }
    else {
      this.onSetResult(defaultUserData)
    }
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevProps.needUpdate && !this.props.needUpdate || prevProps.location.pathname !== this.props.location.pathname) {
      this.componentDidMount()
    }
  }

  getCurrentUser = (id) => {
    fetch(`${config.API_BASE_URL}/user/profile/${id}`, {
      headers: {
        'Content-Type': 'application/json-patch+json'
      }
    })
      .then(res => res.json())
      .then(res => this.onSetResult(res))
      .catch(error => error)
  };

  onSetResult = (result) => {
    this.setState({
      userProfile: result.userProfile ? result.userProfile : {},
      teacherProfile: result.teacherProfile,
      imageUrl: result.userProfile.avatarLink,
      nameInput: result.userProfile.name,
      genderInput: result.userProfile.gender === 1 ? 'Man' : result.userProfile.gender === 2 ? 'Woman' : 'Unknown',
      birthYearInput: result.userProfile.birthYear && result.userProfile.birthYear !== '0001-01-01T00:00:00+00:00' ? result.userProfile.birthYear : this.state.currentDate,
      aboutInput: result.userProfile.aboutUser ? result.userProfile.aboutUser : '',
      contactsInputs: result.userProfile.contacts ? result.userProfile.contacts : [],
      isCurrentUser: Boolean(this.state.userData && this.props.match.params.id == this.state.userData.UserId)
    });
  };

  onChangeNameHandle = (e) => {
    this.setState({nameInput: e.target.value})
  };

  onChangeGenderHandle = (e) => {
    this.setState({genderInput: e})
  };

  onChangeBirthYearHandle = (date, dateString) => {
    this.setState({birthYearInput: date});
  };

  onChangeAboutHandle = (e) => {
    this.setState({aboutInput: e.target.value})
  };

  addContact = () => {
    this.setState({contactsInputs: this.state.contactsInputs.concat('')})
  };

  removeContact = (i) => {
    this.setState({contactsInputs: this.state.contactsInputs.filter((item, index) => index !== i)});
  };

  onHandleChangeContact = (e, i) => {
    this.setState({contactsInputs: this.state.contactsInputs.map((item, index) =>
      index === i ? e.target.value : item
    )})
  };

  cancelChanges = () => {
    this.setState({
      isEditing: false,
      nameInput: this.state.userProfile.name,
      aboutInput: this.state.userProfile.aboutUser,
      birthYearInput: this.state.userProfile.birthYear !== '0001-01-01T00:00:00+00:00' ? this.state.userProfile.birthYear : this.state.currentDate,
      contactsInputs: this.state.userProfile.contacts ? this.state.userProfile.contacts : [],
      genderInput: this.state.userProfile.gender === 1 ? 'Man' : this.state.userProfile.gender === 2 ? 'Woman' : 'Unknown',
      imageUrl: this.state.userProfile.avatarLink
    })
  };

  changeProfileData = (e) => {
    e.preventDefault();
    this.props.form.validateFields((err, value) => {
      if(!err) {
        this.setState({contactsInputs: this.state.contactsInputs.filter(item => item !== '')});
        if(this.state.contactsInputs.length !== this.state.userProfile.contacts.length || this.state.contactsInputs.filter((item, i) =>
            item !== this.state.userProfile.contacts[i]
          ).length !== 0 || this.state.aboutInput !== this.state.userProfile.aboutUser || this.state.birthYearInput !== this.state.userProfile.birthYear ||
          getGenderType(this.state.genderInput) !== this.state.userProfile.gender || this.state.nameInput !== this.state.userProfile.name || this.state.imageUrl !== this.state.userProfile.avatarLink) {
          setTimeout(() => this.props.editProfile(this.state.nameInput, this.state.aboutInput, this.state.genderInput, this.state.contactsInputs, this.state.birthYearInput === this.state.currentDate ? '0001-01-01T00:00:00+00:00' : this.state.birthYearInput , this.state.imageUrl ? this.state.imageUrl : ''), 0);
        }
        if(this.state.teacherProfile && this.state.teacherProfile.skills && (this.state.skillsInput.length !== this.state.teacherProfile.skills.length || this.state.teacherProfile && this.state.teacherProfile.skills && this.state.skillsInput.filter((item, i) =>
            item !== this.state.teacherProfile.skills[i]
          ).length !== 0) || this.state.teacherProfile && !this.state.teacherProfile.skills) {
          setTimeout(() => this.props.editSkills(this.state.skillsInput), 0);
        }
        this.setState({isEditing: false});
      }
    })
  };

  onChangeSkillsHandle = (e) => {
    this.setState({skillsInput: e})
  };

  handleAvatarLinkChange = (info) => {
    if (info.file.status === 'uploading') {
      this.setState({
        imageUrl: '',
        loading: true
      });
      return;
    }
    if (info.file.status === 'error') {
      this.setState({ loading: false });
      return;
    }
    if (info.file.status === 'done') {
      this.setState({
        imageUrl: info.file.response.filename,
        loading: false,
      });
    }
  };

  render() {

    const props = {
      name: 'file',
      action: `${config.API_BASE_URL}/file`,
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      },
      accept: "image/*",
      onChange: this.handleAvatarLinkChange,
      showUploadList: false
    };

    const {getFieldDecorator} = this.props.form;

    return (
      <div>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}} style={{marginTop: 40}} className='md-center-container'>
          <Col xs={{span: 24}} md={{span: 11}} lg={{span: 10}} xl={{span: 8}} xxl={{span: 6}} className='lg-center-container-item'>
            <Form className='profile-form' style={{width: '100%'}} onSubmit={this.changeProfileData}>
              <Card
                title={
                  <Row type='flex' align='middle'>
                    <Col span={24} style={{display: 'flex', alignItems: 'center'}}>
                      <Col xs={{span: 8}} lg={{span: 7}}>
                        {
                          this.state.isEditing ?
                            <Upload
                              {...props}
                              style={{display: 'flex', justifyContent: 'center', alignItems: 'center', fontSize: 30, marginRight: 14, width: 50, height: 50, borderRadius: '50%', cursor: 'pointer'}}
                            >
                              {this.state.imageUrl ? <img src={`${config.API_BASE_URL}/file/img/${this.state.imageUrl}`} style={{height: 50, width: 50, borderRadius: '50%'}} alt="" /> : <Icon type={this.state.loading ? 'loading' : 'user'} />}
                            </Upload>
                            :
                            <Avatar
                              src={this.state.userProfile.avatarLink ? `${config.API_BASE_URL}/file/img/${this.state.userProfile.avatarLink}` : ''}
                              style={{minHeight: 50, minWidth: 50, marginRight: 14, borderRadius: '50%'}}
                            >
                            </Avatar>
                        }
                      </Col>
                      <Col className='word-break' xs={{span: 16}} lg={{span: 17}}>
                        {this.state.isEditing ?
                          <FormItem style={{width: '100%', marginBottom: 0}}>
                            {getFieldDecorator('name', {
                              rules: [
                                {required: true, message: 'Пожалуйста, введите свое имя!'},
                                {message: 'Имя должно быть не меньше 3 символов!', min: 3},
                                {message: 'Имя должно быть не больше 70 символов!', max: 70},
                                {message: 'В имени должны быть только буквы!', pattern: /^[a-zA-Zа-яА-Яё\s]+$/}
                              ],
                              initialValue: this.state.nameInput
                            })(
                              <Input style={{width: '100%'}} onChange={this.onChangeNameHandle} placeholder="Так вас будут видеть на сайте"/>)
                            }
                          </FormItem>
                          : this.state.userProfile.name
                        }
                      </Col>
                    </Col>
                  </Row>
                }
                hoverable
                className='profile-card header-font-size-20 without-border-bottom'
              >
                {
                  this.state.isCurrentUser ?
                    <Row style={{marginBottom: 20}}>
                      <div>Email</div>
                      <p style={{fontSize: 16, color: '#000'}}>
                        {this.state.userProfile.email}
                      </p>
                    </Row>
                    : null
                }
                <Row style={{marginBottom: 20}}>
                  <div>Пол</div>
                  <p style={{fontSize: 16, color: '#000'}}>
                    {this.state.isEditing ?
                      <FormItem style={{width: '100%', marginBottom: 0}}>
                        {getFieldDecorator('gender', {
                          rules: [
                            {required: true, message: 'Пожалуйста, введите свой пол!'}
                          ],
                          initialValue: this.state.genderInput
                        })(
                          <Select onChange={this.onChangeGenderHandle} style={{minWidth: 100}}>
                            <Select.Option value='Unknown'>Не скажу</Select.Option>
                            <Select.Option value='Man'>Мужской</Select.Option>
                            <Select.Option value='Woman'>Женский</Select.Option>
                          </Select>)
                        }
                      </FormItem>
                      : getGender(this.state.userProfile.gender)
                    }
                  </p>
                </Row>
                <Row style={{marginBottom: 20}}>
                  <div>Дата рождения</div>
                  <p style={{fontSize: 16, color: '#000'}}>
                    {
                      this.state.isEditing ?
                        <FormItem style={{width: '100%', marginBottom: 0}}>
                          {getFieldDecorator('birthYear', {
                            rules: [
                              {required: true, message: 'Пожалуйста, введите дату рождения!'}
                            ],
                            initialValue: moment(new Date(this.state.birthYearInput).toLocaleString(), 'DD.MM.YYYY')
                          })(
                            <DatePicker onChange={this.onChangeBirthYearHandle} format='DD.MM.YYYY' style={{width: '100%'}}/>)
                          }
                        </FormItem>
                        : this.state.userProfile.birthYear && this.state.userProfile.birthYear !== '0001-01-01T00:00:00+00:00' ?
                        `${new Date(this.state.userProfile.birthYear).getDate() < 10 ?
                          '0' + new Date(this.state.userProfile.birthYear).getDate()
                          : new Date(this.state.userProfile.birthYear).getDate()}.${
                          +new Date(this.state.userProfile.birthYear).getMonth() + 1 < 10 ?
                            '0' + +(new Date(this.state.userProfile.birthYear).getMonth() + 1)
                          : +new Date(this.state.userProfile.birthYear).getMonth() + 1}.${
                          new Date(this.state.userProfile.birthYear).getFullYear() < 1000 ?
                          '000' + new Date(this.state.userProfile.birthYear).getFullYear()
                          : new Date(this.state.userProfile.birthYear).getFullYear()}`: 'Не указано'
                    }
                  </p>
                </Row>
                {this.state.teacherProfile ? (
                  <Row>
                    <Row style={{marginBottom: 20}}>
                      <div>Основные навыки</div>
                      <Row gutter={6}>
                        <p>
                          {this.state.teacherProfile.skills &&
                          this.state.teacherProfile.skills.length !== 0  && !this.state.isEditing ?
                            this.state.teacherProfile.skills.map((item) =>
                              <Link to={`/groups?tags=${item.replace('#', '*')}`} key={item}>{item} </Link>
                            )
                            : this.state.teacherProfile.skills.length === 0 && !this.state.isEditing && !this.state.isCurrentUser ?
                              <div style={{fontSize: 16, color: '#000'}}>Не указано</div>
                              : this.state.isCurrentUser && !this.state.isEditing && this.state.teacherProfile.skills.length === 0 ? (
                                  <div>
                                    <div style={{fontSize: 16, color: '#000'}}>Не указано</div>
                                    <span onClick={() => this.setState({isEditing: true})} style={{color: '#1890ff', marginTop: 4, cursor: 'pointer'}}>
                                  Теперь вы можете указать свои навыки!
                                </span>
                                  </div>
                                )
                                : this.state.isEditing ?
                                  <FormItem style={{width: '100%', marginBottom: 0}}>
                                    {getFieldDecorator('skills', {
                                      rules: [],
                                      initialValue: this.state.teacherProfile.skills
                                    })(
                                      <Select onChange={this.onChangeSkillsHandle} style={{width: '100%'}} onSearch={this.onHandleSearch} mode="tags" placeholder="Введите свои навыки" notFoundContent={null}>
                                        {this.props.tags && this.props.tags.length ?
                                          this.props.tags.map((item, index) =>
                                            <Select.Option key={item}>{item}</Select.Option>
                                          ) : null}
                                      </Select>)
                                    }
                                  </FormItem>
                                  : null
                          }
                        </p>
                      </Row>
                    </Row>
                  </Row>
                ) : null
                }
                <Row style={{marginBottom: 20}}>
                  <div>О себе</div>
                  <p className='word-break' style={{fontSize: 16, color: '#000'}}>
                    {
                      this.state.isEditing ?
                        <FormItem style={{width: '100%', marginBottom: 0}}>
                          {getFieldDecorator('aboutUser', {
                            rules: [
                              {min: 20, message: 'Должно быть не менее 20 символов!'},
                              {max: 3000, message: 'Должно быть не более 3000 символов!'}
                            ],
                            initialValue: this.state.aboutInput
                          })(
                            <Input.TextArea onChange={this.onChangeAboutHandle} autosize/>)
                          }
                        </FormItem>
                        : this.state.userProfile.aboutUser ?
                        this.state.userProfile.aboutUser : 'Не указано'
                    }
                  </p>
                </Row>
                <Row style={{marginBottom: 20}}>
                  <div>Ссылки</div>
                  <p>
                    {this.state.userProfile.contacts && this.state.userProfile.contacts.length !== 0 && !this.state.isEditing
                      ? this.state.userProfile.contacts.map((item, i) =>
                        <a href={item} target='_blank' key={i} className='user-link' style={{fontSize: 16, display: 'block'}}>
                          {item}
                        </a>
                      ) :
                      this.state.isEditing ?
                        <div>
                          {this.state.contactsInputs.map((item, i) =>
                            <div key={i}>
                              <FormItem style={{width: '100%', marginBottom: 0}}>
                                {getFieldDecorator(`link-${i}`, {
                                  rules: [
                                    {pattern: /^(http:\/\/|https:\/\/)(t.me\/|telegram.me\/|vk.com\/|www.facebook.com\/|ok.ru\/profile\/|twitter.com\/#!\/|instagram.com\/|www.tumblr.com\/dashboard\/blog\/|www.behance.net\/|github.com\/)|api.whatsapp.com\/send\?phone=|viber:\/\/chat\?number=/, message: 'Пожалуйста, введите действительное значение ссылки!'}
                                  ]
                                })(
                                  <div>
                                    <Col span={20}>
                                      <Input
                                        placeholder='Ссылка на профиль'
                                        onChange={(e) => this.onHandleChangeContact(e, i)}
                                        value={this.state.contactsInputs[i]}
                                        style={{marginBottom: 8, width: '100%'}}
                                      />
                                    </Col>
                                    <Col span={4} style={{textAlign: 'right'}}>
                                      <Icon
                                        className="dynamic-delete-button"
                                        type="close"
                                        onClick={() => this.removeContact(i)}
                                      />
                                    </Col>
                                  </div>)
                                }
                              </FormItem>
                            </div>
                          )}
                          {
                            this.state.contactsInputs.length < 5 ?
                              <Button type="dashed" onClick={this.addContact} style={{ width: '100%', marginTop: 8 }}>
                                <Icon type="plus" />
                                Добавить ссылку
                              </Button>
                              : null
                          }
                        </div>
                        :
                        <div style={{fontSize: 16, color: '#000'}}>Не указано</div>
                    }
                  </p>
                </Row>
                {this.state.isEditing ?
                  <div>
                    <Col span={24}>
                      <Button type='primary' htmlType='submit' style={{width: '100%'}}>Подтвердить</Button>
                    </Col>
                    <Col span={24}>
                      <Button onClick={this.cancelChanges} style={{marginTop: 6, width: '100%'}}>Отмена</Button>
                    </Col>
                  </div>
                  : null
                }
                {!this.state.isEditing && this.state.isCurrentUser ?
                  <Button onClick={() => this.setState({isEditing: true})} style={{width: '100%', marginTop: 12}}>
                    Редактировать
                  </Button>
                  : null
                }
              </Card>
            </Form>
            {
              this.state.isCurrentUser ?
                <div style={{width: '100%'}}>
                  <Link to='/create_group'>
                    <Button type='primary' style={{width: '100%', marginTop: 12}}>Создать группу</Button>
                  </Link>
                </div>
                : null
            }
            {this.state.isCurrentUser ?
              !this.state.teacherProfile ?
                <Button
                  type='primary'
                  onClick={() => {
                    this.props.makeTeacher();
                  }}
                  style={{width: '100%', marginTop: 12, marginBottom: 40}}
                >
                  Преподавать
                </Button>
                :
                <Button
                  onClick={() => {
                    this.props.makeNotTeacher();
                  }}
                  style={{width: '100%', marginTop: 12, marginBottom: 40}}
                >
                  Не преподавать
                </Button>
              : null
            }
            {this.state.isCurrentUser ? null :
              <Button
                onClick={this.onReportClick}
                style={{width: '100%', marginTop: 12, marginBottom: 40}}
              >
                Пожаловаться
              </Button>
            }
          </Col>
          <Col xs={{span: 24}} md={{span: 12, offset: 1}} lg={{span: 13, offset: 1}} xl={{span: 15, offset: 1}} xxl={{span: 17, offset: 1}} className='lg-center-container-item xs-groups-tabs'>
            <Tabs defaultActiveKey="1" style={{width: '100%', marginBottom: 40}}>
              <TabPane tab="Группы" key="1">
                {this.props.myGroups.length !== 0 ?
                  <div className='cards-holder md-cards-holder-center'>
                    {localStorage.getItem('withoutServer') === 'true' ?
                      defaultMyGroups.map((item, i) =>
                        <Link className='group-card' style={{width: this.state.groupCardWidth}} to={`/group/${item.groupInfo.id}`} key={item.groupInfo.id}>
                          <UnassembledGroupCard {...item}/>
                        </Link>
                      )
                      :
                      this.props.myGroups.map((item, i) =>
                        <Link className='group-card' style={{width: this.state.groupCardWidth}} to={`/group/${item.groupInfo.id}`} key={item.groupInfo.id}>
                          <UnassembledGroupCard {...item}/>
                        </Link>
                      )
                    }
                  </div>
                  :
                  <Row type='flex' justify='center'>
                    Групп пока нет
                  </Row>
                }
              </TabPane>
              {this.state.teacherProfile ? (
                  <TabPane tab="Профиль преподавателя" key="2" className='teacher-panel' style={{margin: '16px 0'}}>
                    {this.state.teacherProfile.reviews && this.state.teacherProfile.reviews.length !== 0 ?
                      this.state.teacherProfile.reviews.map((item, index) =>
                        <Card key={index} hoverable className='teacher-review'>
                          <Row type='flex' justify='space-between' align='middle' style={{marginBottom: 10}}>
                            <Col style={{fontWeight: 700, fontSize: 16}}>{item.fromUser}</Col>
                            <Col style={{opacity: 0.7}}>{`${new Date(item.date).getDate() < 10 ?
                              '0' + new Date(item.date).getDate()
                              : new Date(item.date).getDate()}.${new Date(item.date).getMonth() < 10 ?
                              '0' + new Date(item.date).getMonth()
                              : new Date(item.date).getMonth()}.${new Date(item.date).getFullYear()}`}</Col>
                          </Row>
                          <Row style={{marginBottom: 2}}>
                            <Col style={{fontWeight: 500}}>{item.title}</Col>
                          </Row>
                          <Row>
                            <Col>{item.text}</Col>
                          </Row>
                        </Card>
                      )
                      :
                      <Row type='flex' justify='center'>
                        Отзывов пока нет
                      </Row>
                    }
                  </TabPane>)
                : null
              }
              {this.state.userData && this.props.match.params.id === this.state.userData.UserId && (this.state.userData.Role === 'Admin' || this.state.userData.Role === 'Moderator') ? (
                  <TabPane tab="Панель администратора" key="3" style={{minHeight: 300}}>
                    <Link to={`/admin/${this.props.match.params.id}`} style={{color: '#747474'}}>
                      <div style={{textAlign: 'center', fontSize: 26, padding: '100px 0'}}>
                        <span>Нажмите, чтобы перейти в панель администратора</span>
                        <Icon type="bar-chart" style={{fontSize: 60, marginTop: 20, display: 'block'}}/>
                      </div>
                    </Link>
                  </TabPane>)
                : null
              }
            </Tabs>
          </Col>
        </Col>
        <MakeReportModal visible={this.state.reportVisible} handleCancel={this.handleReportCancel} userId={this.props.match.params.id} username={this.state.userProfile.name}/>
      </div>
    );
  }
}

ProfilePage.propTypes = {
  dispatch: PropTypes.func,
};

ProfilePage.defaultProps = {

};

const mapStateToProps = createStructuredSelector({
  myGroups: makeSelectUserGroups(),
  needUpdate: makeSelectNeedUpdate(),
  tags: makeSelectTags()
});

function mapDispatchToProps(dispatch) {
  return {
    getCurrentUserGroups: (id) => dispatch(getCurrentUserGroups(id)),
    editUsername: (newName) => dispatch(editUsername(newName)),
    editAboutUser: (aboutUser) => dispatch(editAboutUserInfo(aboutUser)),
    editBirthYear: (birthYear) => dispatch(editBirthYear(birthYear)),
    editContacts: (contacts) => dispatch(editContacts(contacts)),
    editGender: (gender) => dispatch(editGender(gender)),
    makeTeacher: () => dispatch(makeTeacher()),
    makeNotTeacher: () => dispatch(makeNotTeacher()),
    editSkills: (skills) => dispatch(editSkills(skills)),
    editProfile: (name, aboutUser, gender, contacts, birthYear, avatarLink) => dispatch(editProfile(name, aboutUser, gender, contacts, birthYear, avatarLink)),
    getTags: (tag) => dispatch(getTags(tag))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'profilePage', reducer });
const withSaga = injectSaga({ key: 'profilePage', saga });


export default compose(
  withReducer,
  withSaga,
  withConnect,
)(Form.create()(ProfilePage));
