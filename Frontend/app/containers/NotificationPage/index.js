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

export class NotificationPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props)
  }

  notifys = [
    {
      senderName: 'Имя отправителя',
      time: new Date().getHours() + ':' + (new Date().getMinutes()<10 ? '0' : '') + new Date().getMinutes(),
      date: new Date().toLocaleDateString(),
      text: 'Текст уведомления',
      readed: false
    },
    {
      senderName: 'Имя отправителя',
      time: new Date().getHours() + ':' + (new Date().getMinutes()<10 ? '0' : '') + new Date().getMinutes(),
      date: new Date().toLocaleDateString(),
      text: 'Текст уведомления',
      readed: true
    },
    {
      senderName: 'Имя отправителя',
      time: new Date().getHours() + ':' + (new Date().getMinutes()<10 ? '0' : '') + new Date().getMinutes(),
      date: new Date().toLocaleDateString(),
      text: 'Текст уведомления',
      readed: true
    }
  ];

  invites = [
    {
      senderName: 'Имя отправителя',
      time: new Date().getHours() + ':' + (new Date().getMinutes()<10 ? '0' : '') + new Date().getMinutes(),
      date: new Date().toLocaleDateString(),
      text: 'Текст уведомления',
      readed: false
    },
    {
      senderName: 'Имя отправителя',
      time: new Date().getHours() + ':' + (new Date().getMinutes()<10 ? '0' : '') + new Date().getMinutes(),
      date: new Date().toLocaleDateString(),
      text: 'Текст уведомления',
      readed: true
    }
  ];

  render() {
    return (
      <div>
        <Row className='notify-tabs'>
          <Col xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 12, offset: 6}}>
            <Tabs defaultActiveKey="1" style={{margin: '30px 0'}}>
              <TabPane tab="Уведомления" key="1" style={{margin: '30px 0'}}>
                {(localStorage.getItem('without_server') === 'true') ?
                  (<div>
                      {this.notifys.map(item =>
                        <NotifyCard {...item}/>
                      )}
                    </div>
                  ) : null
                }
              </TabPane>
              <TabPane tab="Приглашения" key="2" style={{margin: '30px 0'}}>
                {(localStorage.getItem('without_server') === 'true') ?
                  (<div>
                      {this.invites.map(item =>
                        <InviteCard {...item}/>
                      )}
                    </div>
                  ) : null
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
