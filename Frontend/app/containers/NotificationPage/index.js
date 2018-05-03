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
import {Row, Col, Tabs, Button, Icon, Select, Divider, Form} from 'antd';
const TabPane = Tabs.TabPane;
const FormItem = Form.Item;
const Option = Select.Option;

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
      needUpdate: this.props.needUpdate,
      isEditing: false
    };

    this.showSettingBtn = this.showSettingBtn.bind(this);
    this.hideSettingBtn = this.hideSettingBtn.bind(this);
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
      this.props.getNotifies();
      this.props.getInvites();
    }
  }

  showSettingBtn = () => {
    document.getElementById('setting-btn').classList.remove('notify-setting-btn-hidden');
    document.getElementById('setting-btn').classList.add('notify-setting-btn');
  };

  hideSettingBtn = () => {
    document.getElementById('setting-btn').classList.remove('notify-setting-btn');
    document.getElementById('setting-btn').classList.add('notify-setting-btn-hidden');
  };

  render() {
    return (
      <Row className='notify-tabs'>
        {
          !this.state.isEditing ?
            <Col className='notify-area' onMouseEnter={this.showSettingBtn} onMouseLeave={this.hideSettingBtn} xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 12, offset: 6}}>
              <Icon id='setting-btn' onClick={() => this.setState({isEditing: true})} className='notify-setting-btn-hidden' type="setting"/>
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
                      {this.props.notifies.map((item, index) =>
                        <NotifyCard key={index} {...item}/>
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
                        {this.props.invites.map(item =>
                          <InviteCard key={item.id} {...item}/>
                        )}
                      </div>
                    )
                  }
                </TabPane>
              </Tabs>
            </Col>
            :
            <Col xs={{span: 22, offset: 1}} sm={{span: 20, offset: 2}} lg={{span: 12, offset: 6}}>
              <Row style={{textAlign: 'center', marginTop: 40}}><h4 style={{marginBottom: 0}}>Настройка уведомлений</h4></Row>
              <Row><Divider/></Row>
              <Row style={{marginTop: 0}}>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <FormItem
                  labelCol={{
                    xs: { span: 24 },
                    sm: { span: 14 }
                  }}
                  colon={false}
                  label="Уведомления о завершении курса"
                >
                  <Col className='xs-text-align-left'>
                    <Select style={{width: 180}}>
                      <Option value="Default">По умолчанию</Option>
                      <Option value="DontSend">Не отправлять</Option>
                      <Option value="ToMail">На почту</Option>
                      <Option value="OnSite">На сайте</Option>
                      <Option value="Everywhere">Везде</Option>
                    </Select>
                  </Col>
                </FormItem>
                <Col style={{marginTop: 20, textAlign: 'center', marginBottom: 40}}>
                    <Button htmlType="button" onClick={() => this.setState({isEditing: false})} style={{marginRight: '4%'}}>Отменить</Button>
                    <Button type="primary" onClick={() => this.setState({isEditing: false})}>Подтвердить</Button>
                </Col>
              </Row>
            </Col>
        }
      </Row>
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
