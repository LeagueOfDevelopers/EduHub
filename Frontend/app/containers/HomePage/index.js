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
import { makeSelectGroups } from "./selectors";
import reducer from '../GroupsPage/reducer';
import saga from '../GroupsPage/saga';
import { getGroups } from "../GroupsPage/actions";
import {Link} from "react-router-dom";
import { parseJwt } from "../../globalJS";
import {Card, Col, Row, Button, message} from 'antd';
import UnassembledGroupCard from 'components/UnassembledGroupCard';
import AssembledGroupCard from 'components/AssembledGroupCard';
import SigningInForm from 'containers/SigningInForm';

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

    this.state = {
      signInVisible: false
    }
  }

  componentWillMount() {
    if(localStorage.getItem('token') && parseJwt(localStorage.getItem('token')).exp - parseInt(Date.now()/1000) < 0) {
      localStorage.setItem('name', '');
      localStorage.setItem('avatarLink', '');
      localStorage.setItem('token', '');
    }
  }

  componentDidMount() {
    if(localStorage.getItem('without_server') !== 'true') {
      this.props.getUnassembledGroups();
      this.props.getAssembledGroups();
    }
  }

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  render() {
    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Идет набор'
            bordered={false}
            className='unassembled-groups-list font-size-20'
          >
            {(localStorage.getItem('without_server') === 'true') ?
              (
                <div className='cards-holder cards-holder-center'>
                  {unassembledGroups.map((item) =>
                    <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                      <UnassembledGroupCard {...item}/>
                    </Link>
                  )}
                </div>
              )
              :
              (
                <div className='cards-holder cards-holder-center'>
                  {this.props.unassembledGroups.map((item, i) =>
                    i < 8 ?
                      <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                         <UnassembledGroupCard {...item}/>
                       </Link>
                       : null
                   )}
                 </div>
               )
            }
            <Row type='flex' align='middle' style={{marginTop: 30}}>
              <Col className='xs-margin-bottom-14' xs={{span: 24}} sm={{span: 8}} style={{fontSize: 16}}>
                <Link to='/groups/unassembledGroups'>Показать больше</Link>
              </Col>
              {localStorage.getItem('token') ?
                <Col className='xs-text-align-left' xs={{span: 24}} sm={{span: 16}}>
                  <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Не нашли то, что искали?</Col>
                  <Link to='/create_group'><Button type="primary" htmlType="submit">Создать группу</Button></Link>
                </Col>
                :
                <Col className='xs-text-align-left' xs={{span: 24}} sm={{span: 16}}>
                  <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Не нашли то, что искали?</Col>
                  <Button type="primary" onClick={() => this.setState({signInVisible: true})}>Создать группу</Button>
                </Col>
              }
            </Row>
          </Card>
        </Col>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Набранные группы'
            bordered={false}
            className='assembled-groups-list font-size-20'
          >
            {(localStorage.getItem('without_server') === 'true') ?
              (
                <div className='cards-holder cards-holder-center'>
                  {assembledGroups.map((item) =>
                    <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                      <AssembledGroupCard {...item}/>
                    </Link>
                  )}
                </div>
              )
              :
              (
                <div className='cards-holder cards-holder-center'>
                  {this.props.assembledGroups.map((item, i) =>
                    i < 8 ?
                      <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                        <AssembledGroupCard {...item}/>
                      </Link>
                      : null
                  )}
                </div>
              )
            }
            <Row type='flex' align='middle' style={{marginTop: 30}}>
              <Col className='xs-margin-bottom-14' xs={{span: 24}} sm={{span: 8}} style={{fontSize: 16}}>
                <Link to='/groups/assembledGroups'>Показать больше</Link>
              </Col>
              {localStorage.getItem('token') ?
                <Col className='xs-text-align-left' xs={{span: 24}} sm={{span: 16}}>
                  <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
                  <Link to={`/profile/${parseJwt(localStorage.getItem('token')).UserId}`}><Button type="primary">Стать преподавателем</Button></Link>
                </Col>
                :
                <Col className='xs-text-align-left' xs={{span: 24}} sm={{span: 16}}>
                  <Col style={{display: 'inline', fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
                  <Button type="primary" onClick={() => this.setState({signInVisible: true})}>Стать преподавателем</Button>
                </Col>
              }
            </Row>
            <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
          </Card>
        </Col>
      </div>
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
  unassembledGroups: makeSelectGroups('unassembledGroups'),
  assembledGroups: makeSelectGroups('assembledGroups')
});

function mapDispatchToProps(dispatch) {
  return {
    getUnassembledGroups: () => dispatch(getGroups('unassembledGroups')),
    getAssembledGroups: () => dispatch(getGroups('assembledGroups'))
  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'homePage', reducer });
const withSaga = injectSaga({ key: 'homePage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(HomePage);
