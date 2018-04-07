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
import {makeSelectUserGroups, makeSelectNeedUpdate} from './selectors';
import {
  getCurrentUserGroups,
  editUsername,
  editAboutUserInfo,
  editGender,
  editBirthYear,
  editContacts,
  makeTeacher,
  makeNotTeacher,
  editProfile
} from "./actions";
import reducer from './reducer';
import saga from './saga';
import {parseJwt, getGender} from "../../globalJS";
import config from '../../config';
import {Link} from "react-router-dom";
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";
import {Card, Col, Row, Avatar, Tabs, Input, InputNumber, Select, Button, Icon, Upload} from 'antd';
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
      genderInput: '',
      birthYearInput: '',
      aboutInput: '',
      imageUrl: null,
      avatarLoading: false,
      contactsInputs: [],
      userData: localStorage.getItem('token') ? parseJwt(localStorage.getItem('token')) : null,
      isCurrentUser: false
    };

    this.onSetResult = this.onSetResult.bind(this);
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
  }

  componentDidMount() {
    if(localStorage.getItem('without_server') !== 'true') {
      this.props.getCurrentUserGroups(this.props.match.params.id);
      this.getCurrentUser(this.props.match.params.id);
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
      genderInput: result.userProfile.gender === 1 ? 'Man' : result.userProfile.gender === 2 ? 'Woman' : '',
      birthYearInput: result.userProfile.birthYear,
      aboutInput: result.userProfile.aboutUser ? result.userProfile.aboutUser : '',
      contactsInputs: result.userProfile.contacts ? result.userProfile.contacts : [],
      isCurrentUser: Boolean(this.props.match.params.id == this.state.userData.UserId)
    });
  };

  onChangeNameHandle = (e) => {
    this.setState({nameInput: e.target.value})
  };

  onChangeGenderHandle = (e) => {
    this.setState({genderInput: e})
  };

  onChangeBirthYearHandle = (e) => {
    this.setState({birthYearInput: e})
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
      birthYearInput: this.state.userProfile.birthYear,
      contactsInputs: this.state.userProfile.contacts ? this.state.userProfile.contacts : [],
      genderInput: this.state.userProfile.gender === 1 ? 'Man' : this.state.userProfile.gender === 2 ? 'Woman' : 'Unknown',
      imageUrl: this.state.userProfile.avatarLink
    })
  };

  changeProfileData = () => {
    this.setState({contactsInputs: this.state.contactsInputs.filter(item => item !== '')});
    if(this.state.contactsInputs.length !== this.state.userProfile.contacts.length || this.state.contactsInputs.filter((item, i) =>
        item !== this.state.userProfile.contacts[i]
      ).length !== 0 || this.state.aboutInput !== this.state.userProfile.aboutUser || this.state.birthYearInput !== this.state.userProfile.birthYear ||
      this.state.genderInput !== getGender(this.state.userProfile.gender) || this.state.nameInput !== this.state.userProfile.name || `${config.API_BASE_URL}/file/${this.state.imageUrl}` !== this.state.userProfile.avatarLink) {
      setTimeout(() => this.props.editProfile(this.state.nameInput, this.state.aboutInput, this.state.genderInput, this.state.contactsInputs, this.state.birthYearInput, this.state.imageUrl ? `${config.API_BASE_URL}/file/${this.state.imageUrl}` : ''), 0);
    }
    this.setState({isEditing: false});
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

    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40, marginBottom: 40}} className='md-center-container'>
          <Col xs={{span: 24}} md={{span: 10}} lg={{span: 6}} className='lg-center-container-item'>
            <Card
              title={
                <Row type='flex' align='middle'>
                  <Col span={21} style={{display: 'flex', alignItems: 'center'}}>
                    {
                      this.state.isEditing ?
                        <Upload
                          {...props}
                          style={{display: 'flex', justifyContent: 'center', alignItems: 'center', fontSize: 30, marginRight: 14, width: 50, height: 50, borderRadius: '50%', cursor: 'pointer'}}
                        >
                          {this.state.imageUrl ? <img src={`${config.API_BASE_URL}/file/${this.state.imageUrl}`} style={{height: 50, width: 50, borderRadius: '50%'}} alt="" /> : <Icon type={this.state.loading ? 'loading' : 'plus'} />}
                        </Upload>
                        :
                        <Avatar
                          src={this.state.userProfile.avatarLink ? this.state.userProfile.avatarLink : ''}
                          style={{minHeight: 50, minWidth: 50, marginRight: 14, borderRadius: '50%'}}
                        >
                        </Avatar>
                    }
                    <span>
                      {this.state.isEditing ?
                        <Input style={{width: '100%'}} onChange={this.onChangeNameHandle} value={this.state.nameInput}/>
                        : this.state.userProfile.name
                      }
                    </span>
                  </Col>
                  {!this.state.isEditing && this.state.isCurrentUser ?
                    <Col span={3} style={{textAlign: 'right'}}>
                      <img src={require('../../images/edit.svg')} onClick={() => this.setState({isEditing: true})} style={{width: 20, cursor: 'pointer'}}/>
                    </Col>
                    : null
                  }
                </Row>
              }
              hoverable
              className='profile-card header-font-size-20 without-border-bottom'
            >
              <Row style={{marginBottom: 20}}>
                <div>Почтовый адрес</div>
                <p style={{fontSize: 16, color: '#000'}}>
                  {this.state.userProfile.email}
                </p>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Пол</div>
                <p style={{fontSize: 16, color: '#000'}}>
                  {this.state.isEditing ?
                    <Select onChange={this.onChangeGenderHandle} value={this.state.genderInput} style={{minWidth: 100}}>
                      <Select.Option value='Man'>Мужской</Select.Option>
                      <Select.Option value='Woman'>Женский</Select.Option>
                    </Select>
                    : getGender(this.state.userProfile.gender)
                  }
                </p>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Год рождения</div>
                <p style={{fontSize: 16, color: '#000'}}>
                  {
                    this.state.isEditing ?
                      <InputNumber onChange={this.onChangeBirthYearHandle} style={{width: 150}} value={this.state.birthYearInput}/>
                      : this.state.userProfile.birthYear ?
                      this.state.userProfile.birthYear : 'Не указано'
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
                        this.state.teacherProfile.skills.length !== 0 ?
                          this.state.teacherProfile.skills.map((item) =>
                            <Link to="#" key={item}>{item}</Link>
                          )
                          :
                          !this.state.isEditing && this.state.isCurrentUser ? (
                              <div>
                                <div style={{fontSize: 16, color: '#000'}}>Не указано</div>
                                <span onClick={() => this.setState({isEditing: true})} style={{color: '#52c41a', marginTop: 4, cursor: 'pointer'}}>
                                Теперь вы можете указать свои навыки!
                              </span>
                              </div>
                            )
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
                      <Input.TextArea onChange={this.onChangeAboutHandle} defaultValue={this.state.aboutInput} autosize/>
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
                      <Link to='#' key={i} className='user-link' style={{fontSize: 16, display: 'block'}}>
                        {item}
                      </Link>
                    ) :
                    this.state.isEditing ?
                      <div>
                        {this.state.contactsInputs.map((item, i) =>
                          <div key={i}>
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
                                type="minus-circle-o"
                                onClick={() => this.removeContact(i)}
                              />
                            </Col>
                          </div>
                        )}
                        <Button type="dashed" onClick={this.addContact} style={{ width: '100%', marginTop: 8 }}>
                          <Icon type="plus" />
                          Добавить ссылку
                        </Button>
                      </div>
                      :
                      <div style={{fontSize: 16, color: '#000'}}>Не указано</div>
                  }
                </p>
              </Row>
              {this.state.isEditing ?
                <div>
                  <Col span={24}>
                    <Button type='primary' onClick={this.changeProfileData} style={{width: '100%'}}>Подтвердить</Button>
                  </Col>
                  <Col span={24}>
                    <Button type='danger' onClick={this.cancelChanges} style={{marginTop: 6, width: '100%'}}>Отмена</Button>
                  </Col>
                </div>
                : null
              }
            </Card>
            {
              this.state.isCurrentUser ?
                <Link to='/create_group'>
                  <Button type='primary' size='large' style={{width: '100%', marginTop: 20, minWidth: 280}}>Создать группу</Button>
                </Link>
                : null
            }
            {this.state.isCurrentUser ?
              !this.state.teacherProfile ?
                <Button
                  type='primary'
                  onClick={() => {
                    this.props.makeTeacher();
                  }}
                  style={{width: '100%', marginTop: 12, minWidth: 280}}
                >
                  Стать преподавателем
                </Button>
                :
                <Button
                  onClick={() => {
                    this.props.makeNotTeacher();
                  }}
                  style={{width: '100%', marginTop: 12, minWidth: 280}}
                >
                  Стать учеником
                </Button>
              : null
            }
          </Col>
          <Col xs={{span: 24}} md={{span: 12, offset: 2}} lg={{span: 15, offset: 3}} className='lg-center-container-item xs-groups-tabs'>
            <Tabs defaultActiveKey="1" type='card'>
              <TabPane tab="Группы" key="1">
                {(
                  <div className='cards-holder md-cards-holder-center' style={{margin: '30px 0'}}>
                    {localStorage.getItem('withoutServer') === 'true' ?
                      defaultMyGroups.map((item, i) =>
                        <Link to={`/group/${item.groupInfo.id}`} key={item.groupInfo.id}>
                          <UnassembledGroupCard {...item}/>
                        </Link>
                      )
                      :
                      this.props.myGroups.map((item, i) =>
                        <Link to={`/group/${item.groupInfo.id}`} key={item.groupInfo.id}>
                          <UnassembledGroupCard {...item}/>
                        </Link>
                      )
                    }
                  </div>
                )}
              </TabPane>
              {this.state.teacherProfile ? (
                <TabPane tab="Профиль преподавателя" key="3">

                </TabPane>
              ) : null
              }
              {this.state.userData && this.props.match.params.id === this.state.userData.UserId ? (
                <TabPane tab="Панель администратора" key="3" style={{minHeight: 300}}>
                  <Link to={`/admin/${this.props.match.params.id}`} style={{color: '#747474'}}>
                    <div style={{textAlign: 'center', fontSize: 26, padding: '100px 0'}}>
                      <span>Нажмите, чтобы перейти в панель администратора</span>
                      <Icon type="bar-chart" style={{fontSize: 60, marginTop: 20, display: 'block'}}/>
                    </div>
                  </Link>
                </TabPane>
              ) : null
              }
            </Tabs>
          </Col>
        </Col>
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
  needUpdate: makeSelectNeedUpdate()
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
    editProfile: (name, aboutUser, gender, contacts, birthYear, avatarLink) => dispatch(editProfile(name, aboutUser, gender, contacts, birthYear, avatarLink))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'profilePage', reducer });
const withSaga = injectSaga({ key: 'profilePage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(ProfilePage);
