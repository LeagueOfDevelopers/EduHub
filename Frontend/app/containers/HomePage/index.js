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
import { makeSelectAssembledGroups,
         makeSelectUnassembledGroups
} from "./selectors";
import reducer from './reducer';
import saga from './saga';
import { getAssembledGroups, getUnassembledGroups } from "./actions";
import {Link} from "react-router-dom";
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

    this.makeTeacher = this.makeTeacher.bind(this);
    this.handleCancel = this.handleCancel.bind(this);

    this.state = {
      signInVisible: false,
    }
  }

  componentDidMount() {
    if(localStorage.getItem('without_server') !== 'true') {
      this.props.getUnassembledGroups();
      this.props.getAssembledGroups();
    }
  }

  makeTeacher() {
    if(localStorage.getItem('token'))
      message.success('Теперь вы можете преподавать!');
    else {
      this.setState({signInVisible: true})
  }}

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  render() {
    return (
      <div>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Незаполненные группы'
            bordered={false}
            className='unassembled-groups-list font-size-20'
            extra={<Link to='/groups/unassembledGroups' >Показать больше</Link>}
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
              ) :
              (
                <div className='cards-holder cards-holder-center'>
                  {this.props.unassembledGroups.map((item) =>
                    <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                      <UnassembledGroupCard {...item}/>
                    </Link>
                  )}
                </div>
              )
            }
            <Row type='flex' justify='end' align='middle' style={{marginTop: 30}}>
              <Col style={{fontSize: 18, marginRight: '2%'}}>Не нашли то, что искали?</Col>
              <Link to='/create_group'><Button type="primary" htmlType="submit">Создать группу</Button></Link>
            </Row>
          </Card>
        </Col>
        <Col span={20} offset={2} style={{marginTop: 40}}>
          <Card
            title='Заполненные группы'
            bordered={false}
            className='assembled-groups-list font-size-20'
            extra={<Link to='/groups/assembledGroups'>Показать больше</Link>}
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
              ) :
              (
                <div className='cards-holder cards-holder-center'>
                  {this.props.assembledGroups.map((item) =>
                    <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                      <AssembledGroupCard {...item}/>
                    </Link>
                  )}
                </div>
              )
            }
            <Row type='flex' justify='end' align='middle' style={{marginTop: 30}}>
              <Col style={{fontSize: 18, marginRight: '2%'}}>Уже знаете, чему будете учить?</Col>
              <Button type="primary" onClick={this.makeTeacher}>Стать преподавателем</Button>
              <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
            </Row>
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
  unassembledGroups: makeSelectUnassembledGroups(),
  assembledGroups: makeSelectAssembledGroups()
});

function mapDispatchToProps(dispatch) {
  return {
    getUnassembledGroups: () => dispatch(getUnassembledGroups()),
    getAssembledGroups: () => dispatch(getAssembledGroups())
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
