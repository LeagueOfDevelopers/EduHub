/**
 *
 * NotificationPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import makeSelectNotificationPage from './selectors';
import reducer from './reducer';
import saga from './saga';

import {Row, Col, Tabs,} from 'antd';
const TabPane = Tabs.TabPane;

import NotifyCard from '../../components/NotifyCard';
import InviteCard from '../../components/InviteCard';
import config from "../../config";

export class NotificationPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      notifies: [],
      invites: []
    }
  }

  notifies = [
    {
      fromUser: 'Имя отправителя',
      date: new Date().toDateString(),
      text: 'Текст уведомления',
      readed: false
    },
    {
      fromUser: 'Имя отправителя',
      date: new Date().toDateString(),
      text: 'Текст уведомления',
      readed: true
    },
    {
      fromUser: 'Имя отправителя',
      date: new Date().toDateString(),
      text: 'Текст уведомления',
      readed: true
    }
  ];

  invites = [
    {
      fromUser: 'Имя отправителя',
      date: new Date().toDateString(),
      groupId: '32478643981654',
      suggestedRole: 'Участник',
      readed: false
    },
    {
      fromUser: 'Имя отправителя',
      date: new Date().toDateString(),
      groupId: 'dr32847363274',
      suggestedRole: 'Участник',
      readed: true
    }
  ];

  componentDidMount() {
    if(localStorage.getItem('without_server') === 'true') {
      this.setState({
        notifies: this.notifies,
        invites: this.invites
      })
    }
    else {
      fetch(`${config.API_BASE_URL}/user/profile/notifies`, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
        .then(response => response.json())
        .then(result => {
          this.setState({notifies: result});
        })
        .catch(error => error);

      fetch(`${config.API_BASE_URL}/user/profile/invitations`, {
        headers: {
          'Content-Type': 'application/json-patch+json',
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      })
        .then(response => response.json())
        .then(result => {
          this.setState({invites: result.invitations});
        })
        .catch(error => error);
    }

    // setTimeout(() => console.log(this.state.invites), 300)
  }

  render() {
    return (
      <div>
        <Row className='notify-tabs'>
          <Col xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 12, offset: 6}}>
            <Tabs defaultActiveKey="1" style={{margin: '30px 0'}}>
              <TabPane tab="Уведомления" key="1" style={{margin: '30px 0'}}>
                {(<div>
                      {this.state.notifies.reverse().map(item =>
                        <NotifyCard key={item.id} {...item}/>
                      )}
                    </div>
                  )
                }
              </TabPane>
              <TabPane tab="Приглашения" key="2" style={{margin: '30px 0'}}>
                {(<div>
                      {this.state.invites.reverse().map(item =>
                        <InviteCard key={item.id} {...item}/>
                      )}
                    </div>
                  )
                }
              </TabPane>
            </Tabs>
          </Col>
        </Row>
      </div>
    );
  }
}

NotificationPage.propTypes = {
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

const withReducer = injectReducer({ key: 'notificationPage', reducer });
const withSaga = injectSaga({ key: 'notificationPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(NotificationPage);
