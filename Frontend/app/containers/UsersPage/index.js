/**
 *
 * UsersPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import { makeSelectUsers } from './selectors';
import reducer from './reducer';
import saga from './saga';
import {Row, Col, Card} from 'antd';
import UserCard from '../../components/UserCard';
import FilterForm from '../../components/UsersFilterForm';
import {getQueryVariable} from "../../globalJS";
import SigningInForm from '../../containers/SigningInForm';

const users = [
  {
    id: 'fdsfsdf',
    avatarLink: 'fasfaf',
    name: 'Creator',
    mail: 'creator@creator.creator'
  },
  {
    id: 'dqweqw',
    avatarLink: 'sfdsfsdfdsf',
    name: 'Invited',
    mail: 'invited@invited.invited'
  }
];

export class UsersPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      name: getQueryVariable('name'),
      signInVisible: false,
    };

    this.showFilterForm = this.showFilterForm.bind(this);
    this.addListener = this.addListener.bind(this);
    this.showLoginForm = this.showLoginForm.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  componentDidMount() {

  }

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  showLoginForm = () => {
    this.setState({signInVisible: true})
  };

  addListener = () => {
    let usersLink = Array.from(document.getElementsByClassName('user-link'));
    usersLink.map(item => item.addEventListener('click', (e) => {
      if(!localStorage.getItem('token')) {
        e.preventDefault();
        this.showLoginForm();
      }
    }))
  };

  showFilterForm = () => {
    document.getElementById('xs-filter').style.display === 'block' ?
      document.getElementById('xs-filter').style.display = 'none'
      : document.getElementById('xs-filter').style.display = 'block'
  };

  render() {
    setTimeout(() => this.addListener(), 0);
    return (
      <Row style={{margin: '40px 0'}}>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}} onClick={this.showFilterForm} className='filter-btn' style={{height: 50}}>
          <Card
            hoverable
            style={{cursor: 'pointer', width: '100%', height: '100%'}}
          >
            <span style={{color: '#000'}}>Сортировка</span>
          </Card>
        </Col>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}}>
          <FilterForm id='xs-filter' name={this.state.name} style={{width: '100%'}}/>
        </Col>
        <FilterForm name={this.state.name} id='lg-filter' style={{marginBottom: 40}}/>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}} lg={{span: 9, offset: 1}} xl={{span: 10, offset: 1}} xxl={{span: 11, offset: 1}} className='users-content' style={{marginBottom: 40}}>
          <Row style={{marginBottom: 28}}><h3 style={{marginBottom: 0}}>Пользователи</h3></Row>
          {localStorage.getItem('without_server') !== 'true' ?
            (<div>
                {this.props.users && this.props.users.length && this.props.users.length !== 0 ?
                  this.props.users.map(item =>
                    <UserCard className='user' key={item.id}  {...item}/>
                  )
                  :
                  <div>Нет результатов</div>
                }
              </div>
            )
            :
            (<div>
                {users.map(item =>
                  <UserCard className='user' key={item.id} {...item}/>
                )}
                </div>
            )
          }
        </Col>
        <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
      </Row>
    );
  }
}

UsersPage.propTypes = {

};

const mapStateToProps = createStructuredSelector({
  users: makeSelectUsers(),
});

function mapDispatchToProps(dispatch) {
  return {

  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'usersPage', reducer });
const withSaga = injectSaga({ key: 'usersPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(UsersPage);
