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
import makeSelectUsersPage from './selectors';
import reducer from './reducer';
import saga from './saga';
import {Row, Col, Card} from 'antd';
import UserCard from '../../components/UserCard';
import FilterForm from '../../components/FilterForm';

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

    this.showFilterForm = this.showFilterForm.bind(this);
  }

  componentDidMount() {
    // console.log(this.props.name)
  }

  showFilterForm = () => {
    document.getElementById('xs-filter').style.display === 'none' ?
      document.getElementById('xs-filter').style.display = 'block'
      : document.getElementById('xs-filter').style.display = 'none'
  };

  render() {
    return (
      <Row style={{margin: '40px 0'}}>
        <Col xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} onClick={this.showFilterForm} className='filter-btn' style={{height: 50}}>
          <Card
            hoverable
            style={{cursor: 'pointer', width: '100%', height: '100%'}}
          >
            <span style={{color: '#000'}}>Сортировка</span>
          </Card>
        </Col>
        <FilterForm id='xs-filter'/>
        <FilterForm id='lg-filter'/>
        <Col xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 11, offset: 2}} xl={{span: 12, offset: 2}} xxl={{span: 13, offset: 2}} className='users-content'>
          <Row style={{marginBottom: 28}}><h3 style={{marginBottom: 0}}>Пользователи</h3></Row>
          {localStorage.getItem('without_server') === 'false' ?
            (<div>
                {this.props.users && this.props.users.length !== 0 ?
                  this.props.users.map(item =>
                    <UserCard key={item.id} {...item}/>
                  )
                  :
                  <div>Нет результатов</div>
                }
              </div>
            )
            :
            (<div>
                {users.map(item =>
                  <UserCard key={item.id} {...item}/>
                )}
                </div>
            )
          }
        </Col>
      </Row>
    );
  }
}

UsersPage.propTypes = {
  dispatch: PropTypes.func.isRequired,
};

const mapStateToProps = createStructuredSelector({
  userspage: makeSelectUsersPage(),
});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
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
