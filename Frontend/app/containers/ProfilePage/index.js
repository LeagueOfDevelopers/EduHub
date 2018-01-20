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
import makeSelectProfilePage from './selectors';
import reducer from './reducer';
import saga from './saga';
import {Card, Col, Row, Button, message, Avatar, Tabs} from 'antd';
const TabPane = Tabs.TabPane;
import config from '../../config';
import {Link} from "react-router-dom";
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";

const myGroups = [
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

const createdGroups = [
  {
    groupInfo: {
      id: 2,
      title: 'werfs',
      length: 6,
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf'],
      description: 'dadasddas'
    }
  }
];

export class ProfilePage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      name: '',
      tags: [],
      sex: '',
      years: 0,
      experience: 0,
      description: '',
      links: []
    }
  }

  componentDidMount() {
    if(Boolean(localStorage.getItem('without_server'))) {
      this.setState({
        name: 'Имя пользователя',
        tags: ['js', 'c#'],
        sex: 'Мужской',
        years: 19,
        experience: 3,
        description:
        'Краткая инфа о себе. Краткая инфа о себе. Краткая инфа о себе.\n' +
        '                  Краткая инфа о себе.',
        links: ['LinkedIn', 'Vk']
      })
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
                    src=''
                    style={{minHeight: 50, minWidth: 50, marginRight: 20, borderRadius: '50%'}}
                  >
                  </Avatar>
                  <span>
                    {this.state.name}
                  </span>
                </Row>

              }
              hoverable
              className='profile-card font-size-20 without-border-bottom'
            >
              <Row style={{marginBottom: 20}}>
                <div>Пол</div>
                <div style={{fontSize: 16, color: '#000'}}>{this.state.sex}</div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Возраст</div>
                <div style={{fontSize: 16, color: '#000'}}>{this.state.years} лет</div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Опыт работы</div>
                <div style={{fontSize: 16, color: '#000'}}>{this.state.experience} года</div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Основные навыки</div>
                <Row gutter={6}>
                  {this.state.tags.map((item) =>
                    <Link to="#">{item}</Link>
                  )}
                </Row>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>О себе</div>
                <div style={{fontSize: 16, color: '#000'}}>
                  {this.state.description}
                </div>
              </Row>
              <Row style={{marginBottom: 20}}>
                <div>Ссылки</div>
                <div>
                  {this.state.links.map((item) =>
                    <div>
                      <a className='user-link' style={{fontSize: 16}}>
                        {item}
                      </a>
                    </div>
                  )}
                </div>
              </Row>
            </Card>
          </Col>
          <Col sm={{span: 24}} lg={{span: 15, offset: 3}} className='lg-center-container-item xs-groups-tabs'>
            <Tabs defaultActiveKey="1" type='card'>
              <TabPane tab="Мои группы" key="1">
                {Boolean(localStorage.getItem('without_server')) ?
                  (
                    <div className='cards-holder cards-holder-center' style={{margin: '30px 0'}}>
                      {myGroups.map((item, i) =>
                        <Link to={`/group/${item.groupInfo.id}`}>
                          <UnassembledGroupCard {...item}/>
                        </Link>
                      )}
                    </div>
                  ) : null
                }
              </TabPane>
              <TabPane tab="Созданные группы" key="2">
                {Boolean(localStorage.getItem('without_server')) ?
                  (
                    <div className='cards-holder cards-holder-center' style={{margin: '30px 0'}}>
                      {createdGroups.map((item, i) =>
                        <Link to={`/group/${item.groupInfo.id}`}>
                          <UnassembledGroupCard {...item}/>
                        </Link>
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
  dispatch: PropTypes.func.isRequired,
};

const mapStateToProps = createStructuredSelector({
});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
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
