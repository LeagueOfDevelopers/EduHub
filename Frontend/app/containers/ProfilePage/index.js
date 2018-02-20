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
import {makeSelectUserGroups} from './selectors';
import {
  getCurrentUserGroups,
  editUsername,
  editAboutUserInfo,
  editGender,
  editBirthYear,
  editContacts,
  makeTeacher,
  makeNotTeacher
} from "./actions";
import reducer from './reducer';
import saga from './saga';
import {parseJwt, getGender} from "../../globalJS";
import config from '../../config';
import {Link} from "react-router-dom";
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";
import {Card, Col, Row, Avatar, Tabs, Input, InputNumber, Select, Button, Icon} from 'antd';
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
      contactsInputs: [],
      needUpdate: false
    };

    this.onSetResult = this.onSetResult.bind(this);
    this.getCurrentUser = this.getCurrentUser.bind(this);
    this.onChangeNameHandle = this.onChangeNameHandle.bind(this);
    this.onChangeGenderHandle = this.onChangeGenderHandle.bind(this);
    this.onChangebirthYearHandle = this.onChangebirthYearHandle.bind(this);
    this.onChangeAboutHandle = this.onChangeAboutHandle.bind(this);
    this.changeProfileData = this.changeProfileData.bind(this);
    this.addContact = this.addContact.bind(this);
    this.removeContact = this.removeContact.bind(this);
    this.cancelChanges = this.cancelChanges.bind(this);
  }

  componentDidMount() {
    if(localStorage.getItem('without_server') !== 'true') {
      this.props.getCurrentUserGroups(this.state.id);
      this.getCurrentUser(this.state.id);
    }
    else {
      this.onSetResult(defaultUserData)
    }
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevState.needUpdate !== this.state.needUpdate) {
      this.getCurrentUser(this.state.id);
      this.setState({needUpdate: false});
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
      nameInput: result.userProfile.name,
      genderInput: getGender(result.userProfile.gender),
      birthYearInput: result.userProfile.birthYear,
      aboutInput: result.userProfile.aboutUser,
      contactsInputs: result.userProfile.contacts ? result.userProfile.contacts : []
    });
  };

  onChangeNameHandle = (e) => {
    this.setState({nameInput: e.target.value})
  };

  onChangeGenderHandle = (e) => {
    this.setState({genderInput: e})
  };

  onChangebirthYearHandle = (e) => {
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
      contactsInputs: this.state.userProfile.contacts ? this.state.userProfile.contacts : []
    })
  };

  changeProfileData = () => {
    this.setState({contactsInputs: this.state.contactsInputs.filter(item => item !== '')});
    if(this.state.nameInput !== this.state.userProfile.name) {
      this.props.editUsername(this.state.nameInput);
      localStorage.setItem('name', `${this.state.nameInput}`);
    }
    if(this.state.aboutInput !== this.state.userProfile.aboutUser) {
      this.props.editAboutUser(this.state.aboutInput);
    }
    if(this.state.birthYearInput !== this.state.userProfile.birthYear) {
      this.props.editBirthYear(this.state.birthYearInput);
    }
    if(this.state.genderInput !== getGender(this.state.userProfile.gender)) {
      this.props.editGender(this.state.genderInput);
    }
    if(this.state.contactsInputs.length !== this.state.userProfile.contacts.length || this.state.contactsInputs.filter((item, i) =>
        item !== this.state.userProfile.contacts[i]
      ).length !== 0) {
      this.props.editContacts(this.state.contactsInputs)
    }
    this.setState({isEditing: false});
    this.setState({needUpdate: true})
  };

  render() {
    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40, marginBottom: 40}} className='md-center-container'>
          <Col xs={{span: 24}} md={{span: 10}} lg={{span: 6}} className='lg-center-container-item'>
            <Card
              title={
                <Row type='flex' align='middle'>
                  <Col span={21} style={{display: 'flex', alignItems: 'center'}}>
                    <Avatar
                      src={this.state.userProfile.avatarLink}
                      style={{minHeight: 50, minWidth: 50, marginRight: 20, borderRadius: '50%'}}
                    >
                    </Avatar>
                    <span>
                    {this.state.isEditing ?
                      <Input style={{width: '86%'}} onChange={this.onChangeNameHandle} value={this.state.nameInput}/>
                      : this.state.userProfile.name}
                  </span>
                  </Col>
                  {localStorage.getItem('token') && !this.state.isEditing && parseJwt(localStorage.getItem('token')).UserId === this.state.id ?
                    <Col span={3} style={{textAlign: 'right'}}>
                      <img src={require('../../images/edit.svg')} onClick={() => this.setState({isEditing: true})} style={{width: 20, cursor: 'pointer'}}/>
                    </Col>
                    : null
                  }
                </Row>
              }
              hoverable
              className='profile-card font-size-20 without-border-bottom'
            >
              <Row style={{marginBottom: 20}}>
                <div>Почтовый адрес</div>
                <div style={{fontSize: 16, color: '#000'}}>
                  {this.state.userProfile.email}
                </div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Пол</div>
                <div style={{fontSize: 16, color: '#000'}}>
                  {this.state.isEditing ?
                    <Select onChange={this.onChangeGenderHandle} value={this.state.genderInput} style={{minWidth: 100}}>
                      <Select.Option value='Man'>Мужской</Select.Option>
                      <Select.Option value='Woman'>Женский</Select.Option>
                    </Select>
                    : getGender(this.state.userProfile.gender)
                  }
                </div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Год рождения</div>
                <div style={{fontSize: 16, color: '#000'}}>
                  {
                    this.state.isEditing ?
                      <InputNumber onChange={this.onChangebirthYearHandle} style={{width: 150}} value={this.state.birthYearInput}/>
                      : this.state.userProfile.birthYear ?
                      this.state.userProfile.birthYear : 'Не указано'
                  }
                </div>
              </Row>
              {this.state.teacherProfile ? (
                <Row>
                  <Row style={{marginBottom: 20}}>
                    <div>Основные навыки</div>
                    <Row gutter={6}>
                      {this.state.teacherProfile.skills &&
                      this.state.teacherProfile.skills.length !== 0 ?
                        this.state.teacherProfile.skills.map((item) =>
                          <Link to="#" key={item}>{item}</Link>
                        )
                        :
                        !this.state.isEditing ? (
                            <div>
                              <div style={{fontSize: 16, color: '#000'}}>Не указано</div>
                              <span onClick={() => this.setState({isEditing: true})} style={{color: '#52c41a', marginTop: 4, cursor: 'pointer'}}>
                                Теперь вы можете указать свои навыки!
                              </span>
                            </div>
                          )
                          : null
                      }
                    </Row>
                  </Row>
                </Row>
              ) : null
              }
              <Row style={{marginBottom: 20}}>
                <div>О себе</div>
                <div style={{fontSize: 16, color: '#000'}}>
                  {
                    this.state.isEditing ?
                      <Input.TextArea onChange={this.onChangeAboutHandle} defaultValue={this.state.aboutInput} autosize/>
                      : this.state.userProfile.aboutUser ?
                      this.state.userProfile.aboutUser : 'Не указано'
                  }
                </div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Ссылки</div>
                <div>
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
                </div>
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
            <Link to='/create_group'>
              <Button type='primary' size='large' style={{width: '100%', marginTop: 20, minWidth: 280}}>Создать группу</Button>
            </Link>
            {!this.state.teacherProfile ?
              <Button
                type='primary'
                onClick={() => {
                  this.props.makeTeacher();
                  this.setState({needUpdate: true})
                }}
                style={{width: '100%', marginTop: 12, minWidth: 280}}
              >
                Стать преподавателем
              </Button>
              :
              <Button
                onClick={() => {
                  this.props.makeNotTeacher();
                  this.setState({needUpdate: true})
                }}
                style={{width: '100%', marginTop: 12, minWidth: 280}}
              >
                Стать учеником
              </Button>
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
  myGroups: makeSelectUserGroups()
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
    makeNotTeacher: () => dispatch(makeNotTeacher())
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
