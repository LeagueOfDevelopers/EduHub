/*
 * HomePage
 *
 * This is the first thing users see of our App, at the '/' route
 *
 * NOTE: while this component should technically be a stateless functional
 * component (SFC), hot reloading does not currently support SFCs. If hot
 * reloading is not a necessity for you then you can refactor it and remove
 * the linting exception.
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import { makeSelectGroups } from "../GroupsPage/selectors";
import reducer from '../GroupsPage/reducer';
import saga from '../GroupsPage/saga';
import { getFilteredGroups } from "../GroupsPage/actions";
import {Link} from "react-router-dom";
import { parseJwt, getGroupCardWidth } from "../../globalJS";
import {Card, Col, Row, Button, message} from 'antd';
import UnassembledGroupCard from 'components/UnassembledGroupCard';
import AssembledGroupCard from 'components/AssembledGroupCard';
import SigningInForm from 'containers/SigningInForm';
import config from "../../config";

const unassembledGroups = [
  {
    groupInfo: {
      id: 1,
      title: 'cdcvvdsc',
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf']
    },
    numberOfMembers: 6,
  }];

const assembledGroups = [
  {
    groupInfo: {
      id: 2,
      title: 'cdcvvdsc',
      size: 8,
      moneyPerUser: 600,
      groupType: 'Lfdsv',
      tags: ['fds', 'sdf'],
      description: 'dadasddas'
    },
    numberOfMembers: 6,
  }
];

export class HomePage extends React.PureComponent { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.handleCancel = this.handleCancel.bind(this);
    this.getGroups = this.getGroups.bind(this);
    this.showLoginForm = this.showLoginForm.bind(this);

    this.state = {
      signInVisible: false,
      groupCardWidth: '100%',
      unassembledGroups: [],
      assembledGroups: []
    }
  }

  showLoginForm = () => {
    this.setState({signInVisible: true})
  };

  componentWillMount() {
    if(localStorage.getItem('token') && parseJwt(localStorage.getItem('token')).exp - parseInt(Date.now()/1000) < 0) {
      localStorage.setItem('name', '');
      localStorage.setItem('avatarLink', '');
      localStorage.setItem('token', '');
      location.reload();
    }
  }

  componentDidMount() {
    this.setState({groupCardWidth: getGroupCardWidth()});
    if(localStorage.getItem('without_server') !== 'true') {
      this.getGroups();
    }
  }

  getGroups = () => {
    return fetch(`${config.API_BASE_URL}/group`)
      .then(response => response.json())
      .then(res => {
        this.setState(
          {
            unassembledGroups: res.fillingGroups,
            assembledGroups: res.fullGroups
          }
        );
        setTimeout(() => {
          let groupCards = Array.from(document.getElementsByClassName('group-card'));
          groupCards.map(item => item.addEventListener('click', (e) => {
            if(!localStorage.getItem('token')) {
              e.preventDefault();
              this.showLoginForm();
            }
          }))
        }, 0)
      })
      .catch(error => error)
  };

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  render() {
    return (
      localStorage.getItem('token') && parseJwt(localStorage.getItem('token')).exp - parseInt(Date.now()/1000) > 0 || !localStorage.getItem('token') ?
        (
          <div>
            <Col xs={{span: 22, offset: 1}} sm={{span: 16, offset: 4}} style={{marginTop: 40}}>
              <Card
                title='Группы, которым требуются ученики'
                bordered={false}
                className='unassembled-groups-list font-size-20'
              >
                {(localStorage.getItem('without_server') === 'true') ?
                  (
                    <div className='cards-holder'>
                      {unassembledGroups.map((item) =>
                        <Link className='group-card' style={{width: this.state.groupCardWidth}} key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                          <UnassembledGroupCard {...item}/>
                        </Link>
                      )}
                    </div>
                  )
                  :
                  (
                    <div className='cards-holder'>
                      {this.state.unassembledGroups.map((item, i) =>
                        i < 8 ?
                          <Link className='group-card' style={{width: this.state.groupCardWidth}} key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                            <UnassembledGroupCard {...item}/>
                          </Link>
                          : null
                      )}
                    </div>
                  )
                }
                <Row type='flex' align='middle' style={{marginTop: 30}}>
                  <Col className='xs-margin-bottom-14' xs={{span: 24}} md={{span: 8}} style={{fontSize: 16}}>
                    <Link to='/groups?formed=false' style={{color: '#2C365D'}}>Расширенный поиск</Link>
                  </Col>
                  {localStorage.getItem('token') ?
                    <Col className='xs-text-align-left' xs={{span: 24}} md={{span: 16}}>
                      <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Не нашли то, что искали?</Col>
                      <Link to='/create_group'><Button type="primary" htmlType="submit">Создать группу</Button></Link>
                    </Col>
                    :
                    <Col className='xs-text-align-left' xs={{span: 24}} md={{span: 16}}>
                      <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Не нашли то, что искали?</Col>
                      <Button type="primary" onClick={() => this.setState({signInVisible: true})}>Создать группу</Button>
                    </Col>
                  }
                </Row>
              </Card>
            </Col>
            <Col xs={{span: 22, offset: 1}} sm={{span: 16, offset: 4}} style={{marginTop: 20, marginBottom: 40}}>
              <Card
                title='Группы, которым требуются преподаватели'
                bordered={false}
                className='assembled-groups-list font-size-20'
              >
                {(localStorage.getItem('without_server') === 'true') ?
                  (
                    <div className='cards-holder'>
                      {assembledGroups.map((item) =>
                        <Link className='group-card' style={{width: this.state.groupCardWidth}} key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                          <AssembledGroupCard {...item}/>
                        </Link>
                      )}
                    </div>
                  )
                  :
                  (
                    <div className='cards-holder'>
                      {this.state.assembledGroups.map((item, i) =>
                        i < 8 ?
                          <Link className='group-card' style={{width: this.state.groupCardWidth}} key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                            <AssembledGroupCard {...item}/>
                          </Link>
                          : null
                      )}
                    </div>
                  )
                }
                <Row type='flex' align='middle' style={{marginTop: 30}}>
                  <Col className='xs-margin-bottom-14' xs={{span: 24}} md={{span: 8}} style={{fontSize: 16}}>
                    <Link to='/groups?formed=true'  style={{color: '#2C365D'}}>Расширенный поиск</Link>
                  </Col>
                  {!localStorage.getItem('token') ?
                    <Col className='xs-text-align-left' xs={{span: 24}} md={{span: 16}}>
                      <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
                      <Button type="primary" onClick={() => this.setState({signInVisible: true})}>Стать преподавателем</Button>
                    </Col>
                    : localStorage.getItem('isTeacher') !== 'true' ?
                      <Col className='xs-text-align-left' xs={{span: 24}} md={{span: 16}}>
                        <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
                        <Link to={`/profile/${parseJwt(localStorage.getItem('token')).UserId}`}><Button type="primary">Стать преподавателем</Button></Link>
                      </Col>
                      : null
                  }
                </Row>
                <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
              </Card>
            </Col>
          </div>
        )
        : null
    );
  }
}

HomePage.propTypes = {
  makeTeacher: PropTypes.func,
  unassembledGroups: PropTypes.oneOfType([
    PropTypes.array,
    PropTypes.object
  ]),
  assembledGroups: PropTypes.oneOfType([
    PropTypes.array,
    PropTypes.object
  ])
};

const mapStateToProps = createStructuredSelector({
});

function mapDispatchToProps(dispatch) {
  return {
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

export default compose(
  withConnect
)(HomePage);
