/**
 *
 * AdminPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';

import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import makeSelectAdminPage from './selectors';
import reducer from './reducer';
import saga from './saga';
import { Row, Col, Button, List, Avatar, Icon, Popconfirm } from 'antd';
import {Link} from "react-router-dom";
import ReportModal from '../../components/ReportModal';
import SanctionModal from '../../components/SanctionModal';
import MakeSanctionModal from '../../components/MakeSanctionModal';

export class AdminPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      reportVisible: false,
      sanctionVisible: false,
      makeSanctionVisible: false,
    }

    this.onReportClick = this.onReportClick.bind(this);
    this.handleReportCancel = this.handleReportCancel.bind(this);
    this.onSanctionClick = this.onSanctionClick.bind(this);
    this.handleSanctionCancel = this.handleSanctionCancel.bind(this);
    this.onMakeSanctionClick = this.onMakeSanctionClick.bind(this);
    this.handleMakeSanctionCancel = this.handleMakeSanctionCancel.bind(this);
  }

  onReportClick = () => {
    this.setState({reportVisible: true})
  };

  handleReportCancel = () => {
    this.setState({reportVisible: false})
  };

  onSanctionClick = () => {
    this.setState({sanctionVisible: true})
  };

  handleSanctionCancel = () => {
    this.setState({sanctionVisible: false})
  };

  onMakeSanctionClick = () => {
    this.setState({makeSanctionVisible: true})
  };

  handleMakeSanctionCancel = () => {
    this.setState({makeSanctionVisible: false})
  };

  render() {
    return (
      <Col span={20} offset={2} style={{padding: '20px 0'}}>
        <Row type='flex' justify='space-between' align='middle'>
          <Col xs={{span: 24}} md={{span: 14}} lg={{span: 10}} style={{height: 60, fontSize: 22, backgroundColor: 'rgba(0,0,0,0.1)', display: 'flex', justifyContent: 'center', alignItems: 'center', color: 'black'}}>Панель администратора</Col>
          <Col className='sanction-btn' xs={{span: 24}} md={{span: 10}} lg={{span: 14}}><Button onClick={this.onMakeSanctionClick} type='primary' size='large'>Выписать санкцию</Button></Col>
        </Row>
        <Row style={{marginTop: 12}}>
          <Col xs={{span: 24}} md={{span: 8}} lg={{span: 8}} xl={{span: 6}} xxl={{span: 6}} style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16}}>
            <Row type='flex' justify='space-between' style={{padding: '6px 16px', boxShadow: '0px 2px 6px -2px rgba(0,0,0,0.36)'}}>
              <Col style={{textAlign: 'center', width: '100%'}}>Текущие администраторы</Col>
            </Row>
            <List
              className='admin-list'
              dataSource={[
                {
                  name: 'Первый модератор',
                  inviteCode: 'asdkgh-dsffq-qwewqe-123dfs'
                }
              ]}
              renderItem={item => (
                <List.Item key={item.userId}>
                  <List.Item.Meta
                    avatar={
                      <Avatar
                        src={item.avatarLink}
                      />}
                    title={<Link to={`/profile/${item.userId}`}>{item.name}</Link>}
                    description={item.inviteCode}
                  />
                  {true ?
                    (<Popconfirm
                      title='Удалить модератора?'
                      onConfirm={() => console.log('ok')}
                      okText="Да"
                      cancelText="Нет"
                    >
                      <Icon
                        style={{fontSize: 18, cursor: 'pointer'}}
                        type="close"
                      />
                    </Popconfirm>)
                    : null
                  }
                </List.Item>
              )}
              footer={(<Button style={{width: '100%', height: '100%', minHeight: 40}}>Пригласить модератора</Button>)}
            >
            </List>
          </Col>
          <Col className='event-list-container' xs={{span: 24}} md={{span: 15, offset: 1}} lg={{span: 15, offset: 1}} xl={{span: 10, offset: 1}} xxl={{span: 10, offset: 1}} style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16, minHeight: 250}}>
            <Row type='flex' justify='space-between' style={{padding: '6px 16px', boxShadow: '0px 2px 6px -2px rgba(0,0,0,0.36)'}}>
              <Col style={{textAlign: 'center', width: '100%'}}>История событий</Col>
            </Row>
            <List
              className='event-list'
              dataSource={[]}
              renderItem={(item, index) => (
                <div>
                  <List.Item key={item.userId}>
                    <List.Item.Meta
                      title={item.event}
                      description={item.date}
                    >
                    </List.Item.Meta>
                  </List.Item>
                </div>
              )}
            >
            </List>
          </Col>
          <Col xs={{span: 24}} md={{span: 8}} lg={{span: 8}} xl={{span: 6, offset: 1}} xxl={{span: 6, offset: 1}}>
            <Col style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16}}>
              <Row type='flex' justify='space-between' style={{padding: '6px 16px', boxShadow: '0px 2px 6px -2px rgba(0,0,0,0.36)'}}>
                <Col style={{textAlign: 'center', width: '100%'}}>Текущие репорты</Col>
              </Row>
              <List
                className='admin-list'
                dataSource={[
                  {
                    name: 'Первый пользователь',
                    reason: 'Причина репорта'
                  },
                  {
                    name: 'Второй пользователь',
                    reason: 'Причина репорта'
                  }
                ]}
                renderItem={item => (
                  <List.Item key={item.userId}>
                    <List.Item.Meta
                      avatar={
                        <Avatar
                          src={item.avatarLink}
                        />}
                      title={<Link to={`/profile/${item.userId}`}>{item.name}</Link>}
                      description={item.reason}
                    />
                    <Icon
                      style={{fontSize: 18, cursor: 'pointer'}}
                      type="ellipsis"
                      onClick={this.onReportClick}
                    />
                  </List.Item>
                )}
              >
              </List>
            </Col>
            <Col style={{boxShadow: 'rgba(0, 0, 0, 0.4) 0px 0px 6px -2px', marginTop: 16}}>
              <Row type='flex' justify='space-between' style={{padding: '6px 16px', boxShadow: '0px 2px 6px -2px rgba(0,0,0,0.36)'}}>
                <Col style={{textAlign: 'center', width: '100%'}}>Текущие санкции</Col>
              </Row>
              <List
                className='admin-list'
                dataSource={[
                  {
                    name: 'Первый пользователь',
                    reason: 'Запрет на преподавание'
                  },
                  {
                    name: 'Второй пользователь',
                    reason: 'Запрет на преподавание'
                  }
                ]}
                renderItem={item => (
                  <List.Item key={item.userId}>
                    <List.Item.Meta
                      avatar={
                        <Avatar
                          src={item.avatarLink}
                        />}
                      title={<Link to={`/profile/${item.userId}`}>{item.name}</Link>}
                      description={item.reason}
                    />
                    <Icon
                      style={{fontSize: 18, cursor: 'pointer'}}
                      type="ellipsis"
                      onClick={this.onSanctionClick}
                    />
                  </List.Item>
                )}
              >
              </List>
            </Col>
          </Col>
        </Row>
        <SanctionModal visible={this.state.sanctionVisible} handleCancel={this.handleSanctionCancel}/>
        <MakeSanctionModal visible={this.state.makeSanctionVisible} handleCancel={this.handleMakeSanctionCancel}/>
        <ReportModal visible={this.state.reportVisible} onSanctionClick={this.onMakeSanctionClick} handleCancel={this.handleReportCancel}/>
      </Col>
    );
  }
}

AdminPage.propTypes = {

};

const mapStateToProps = createStructuredSelector({

});

function mapDispatchToProps(dispatch) {
  return {
    dispatch,
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'adminPage', reducer });
const withSaga = injectSaga({ key: 'adminPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(AdminPage);
