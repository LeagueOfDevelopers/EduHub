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
import {makeSelectNotifies, makeSelectInvites, makeSelectNeedUpdate} from './selectors';
import reducer from './reducer';
import saga from './saga';
import {getInvites, getNotifies} from "./actions";
import NotifyCard from '../../components/NotifyCard';
import InviteCard from '../../components/InviteCard';
import {Row, Col, Tabs,} from 'antd';
const TabPane = Tabs.TabPane;

const notifies = [
  {
    id: '1233123124',
    fromUser: 'Имя отправителя',
    date: new Date().toDateString(),
    text: 'Текст уведомления',
    readed: false
  },
  {
    id: 'dasqdsad',
    fromUser: 'Имя отправителя',
    date: new Date().toDateString(),
    text: 'Текст уведомления',
    readed: true
  },
  {
    id: 'qwersifhxjk',
    fromUser: 'Имя отправителя',
    date: new Date().toDateString(),
    text: 'Текст уведомления',
    readed: true
  }
];

const invites = [
  {
    id: 'dasqdsadsdfsdf',
    fromUser: 'fdspijfjlkf',
    fromUserName: 'Имя отправителя',
    date: new Date().toDateString(),
    toGroup: '32478643981654',
    toGroupTitle: 'JS Juniors',
    suggestedRole: 'Участник',
    readed: false
  },
  {
    id: 'dasqdsddsfwee',
    fromUser: '324gj3h4j1',
    fromUserName: 'Имя отправителя',
    date: new Date().toDateString(),
    toGroup: 'dr32847363274',
    toGroupTitle: 'C# Juniors',
    suggestedRole: 'Участник',
    readed: true
  }
];

export class NotificationPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      needUpdate: this.props.needUpdate
    };
  }

  componentDidMount() {
    if(localStorage.getItem('without_server') === 'true') {
      this.setState({
        notifies: this.notifies,
        invites: this.invites
      })
    }
    else {
      this.props.getNotifies();
      this.props.getInvites();
    }
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevProps.needUpdate !== this.props.needUpdate) {
      this.props.getInvites();
    }
  }

  render() {
    return (
      <div>
        <Row className='notify-tabs'>
          <Col xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 12, offset: 6}}>
            <Tabs defaultActiveKey="1" style={{margin: '30px 0'}}>
              <TabPane tab="Уведомления" key="1" style={{margin: '30px 0'}}>
                {localStorage.getItem('withoutServer') === 'true' ?
                  (<div>
                    {notifies.reverse().map(item =>
                      <NotifyCard key={item.id} {...item}/>
                    )}
                  </div>
                  )
                  :
                  <div>
                    {this.props.notifies.reverse().map(item =>
                      <NotifyCard key={item.id} {...item}/>
                    )}
                  </div>
                }
              </TabPane>
              <TabPane tab="Приглашения" key="2" style={{margin: '30px 0'}}>
                {localStorage.getItem('withoutServer') === 'true' ?
                  (<div>
                      {invites.reverse().map(item =>
                        <InviteCard key={item.id} {...item}/>
                      )}
                    </div>
                  )
                  :
                  (<div>
                      {this.props.invites.reverse().map(item =>
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
  dispatch: PropTypes.func,
};

const mapStateToProps = createStructuredSelector({
  notifies: makeSelectNotifies(),
  invites: makeSelectInvites(),
  needUpdate: makeSelectNeedUpdate()
});

function mapDispatchToProps(dispatch) {
  return {
    getNotifies: () => dispatch(getNotifies()),
    getInvites: () => dispatch(getInvites())
  }
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'notificationPage', reducer });
const withSaga = injectSaga({ key: 'notificationPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(NotificationPage);
