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
import {getCurrentUserGroups} from "./actions";
import reducer from './reducer';
import saga from './saga';
import config from '../../config';
import {Link} from "react-router-dom";
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";
import {Card, Col, Row, Avatar, Tabs} from 'antd';
const TabPane = Tabs.TabPane;

const defaultUserData = {
  name: 'Имя пользователя',
  tags: ['js', 'c#'],
  sex: 'Мужской',
  years: 19,
  experience: 3,
  description:
  'Краткая инфа о себе. Краткая инфа о себе. Краткая инфа о себе.\n' +
  '                  Краткая инфа о себе.',
  links: ['LinkedIn', 'Vk']
};

export class ProfilePage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      id: this.props.match.params.id,
      userData: {
        name: '',
        email: '',
        avatarLink: '',
        isMan: '',
        birthYear: null,
        aboutUser: '',
        contacts: []
      },
      teacherProfile: {
        reviews: [],
        skills: []
      }
    };
  }

  componentDidMount() {
    if(localStorage.getItem('without_server') === 'true') {
      this.setState({userData: defaultUserData})
    }
    else {
      this.props.getCurrentUserGroups();
      fetch(`${config.API_BASE_URL}/user/profile/${this.state.id}`, {
        headers: {
          'Content-Type': 'application/json-patch+json',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
          .then(res => res.json())
          .then(res => {
            this.setState({
              userData: res.userProfile,
              teacherProfile: res.teacherProfile
            },
              console.log(this.state.userData))
          })
          .catch(error => error)
    }
  }

  render() {
    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40}} className='md-center-container'>
          <Col md={{span: 24}} lg={{span: 6}} className='lg-center-container-item'>
            <Card
              title={
                <Row type='flex' align='middle' style={{textAlign: 'center'}}>
                  <Avatar
                    src={this.state.userData.avatarLink}
                    style={{minHeight: 50, minWidth: 50, marginRight: 20, borderRadius: '50%'}}
                  >
                  </Avatar>
                  <span>
                    {this.state.userData.name}
                  </span>
                </Row>

              }
              hoverable
              className='profile-card font-size-20 without-border-bottom'
            >
              <Row style={{marginBottom: 20}}>
                <div>Почтовый адрес</div>
                <div style={{fontSize: 16, color: '#000'}}>{this.state.userData.email}</div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Пол</div>
                <div style={{fontSize: 16, color: '#000'}}>{this.state.userData.isMan}</div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Возраст</div>
                <div style={{fontSize: 16, color: '#000'}}>{this.state.userData.birthYear} лет</div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Основные навыки</div>
                <Row gutter={6}>
                  {this.state.userData.teacherProfile ?
                    this.state.userData.teacherProfile.skills.map((item) =>
                    <Link to="#" key={item}>{item}</Link>
                  ) : null}
                </Row>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>О себе</div>
                <div style={{fontSize: 16, color: '#000'}}>
                  {this.state.userData.description}
                </div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Ссылки</div>
                <div>
                  {this.state.userData.contacts.map((item) =>
                    <div>
                      <Link to='#' key={item} className='user-link' style={{fontSize: 16}}>
                        {item}
                      </Link>
                    </div>
                  )}
                </div>
              </Row>
            </Card>
          </Col>
          <Col sm={{span: 24}} lg={{span: 15, offset: 3}} className='lg-center-container-item xs-groups-tabs'>
            <Tabs defaultActiveKey="1" type='card'>
              <TabPane tab="Группы" key="1">
                {(localStorage.getItem('without_server') === 'true') ?
                  (
                    <div className='cards-holder md-cards-holder-center' style={{margin: '30px 0'}}>
                      {this.props.myGroups.map((item, i) =>
                        <UnassembledGroupCard {...item}/>
                      )}
                    </div>
                  ) : null
                }
              </TabPane>
              <TabPane tab="Профиль преподавателя" key="3">

              </TabPane>
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
  myGroups: [
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
  ]
};

const mapStateToProps = createStructuredSelector({
  myGroups: makeSelectUserGroups()
});

function mapDispatchToProps(dispatch) {
  return {
    getCurrentUserGroups: () => dispatch(getCurrentUserGroups())
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
