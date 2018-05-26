/**
 *
 * GroupsPage
 *
 */

import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { createStructuredSelector } from 'reselect';
import { compose } from 'redux';
import injectSaga from 'utils/injectSaga';
import injectReducer from 'utils/injectReducer';
import reducer from './reducer';
import saga from './saga';
import { getQueryVariable, getGroupCardWidth } from "../../globalJS";
import {Link} from "react-router-dom";
import { makeSelectGroups } from "./selectors";
import {Row, Col, Menu, Dropdown, Icon, Card} from 'antd';
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";
import FilterForm from '../../components/GroupsFilterForm';
import SigningInForm from '../../containers/SigningInForm';

export class GroupsPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      signInVisible: false,
      groupCardWidth: '100%',
      formed: getQueryVariable('formed') === 'true',
      title: getQueryVariable('name'),
      tags: getQueryVariable('tags')
    };

    this.showFilterForm = this.showFilterForm.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
    this.showLoginForm = this.showLoginForm.bind(this);
  }

  componentDidMount() {
    this.setState({groupCardWidth: getGroupCardWidth()})
  }

  handleCancel = () => {
    this.setState({signInVisible: false})
  };

  showLoginForm = () => {
    this.setState({signInVisible: true})
  };

  componentDidUpdate(prevProps) {
    if(prevProps.location.search !== this.props.location.search) {
      this.setState({tags: getQueryVariable('tags')})
    }
  }

  showFilterForm = () => {
    document.getElementById('xs-filter').style.display === 'block' ?
      document.getElementById('xs-filter').style.display = 'none'
      : document.getElementById('xs-filter').style.display = 'block'
  };

  render() {
    return (
      <Row style={{marginTop: 40, marginBottom: 40}}>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}} onClick={this.showFilterForm} className='filter-btn' style={{height: 50}}>
          <Card
            hoverable
            style={{cursor: 'pointer', width: '100%', height: '100%'}}
          >
            <span style={{color: '#000'}}>Сортировка</span>
          </Card>
        </Col>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}}>
          <FilterForm id='xs-filter' title={this.state.title} queryTags={this.state.tags} formed={this.state.formed} style={{width: '100%'}}/>
        </Col>
        <FilterForm id='lg-filter' title={this.state.title} queryTags={this.state.tags} formed={this.state.formed} style={{marginBottom: 40}}/>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}} lg={{span: 9, offset: 1}} xl={{span: 10, offset: 1}} xxl={{span: 11, offset: 1}} className='groups-content' style={{marginBottom: 40}}>
          <Row type='flex' justify='space-between' align='middle'>
            <h3 style={{marginBottom: 0}}>Группы</h3>
          </Row>
          <Row className='cards-holder font-size-20' style={{marginTop: 28}}>
            {this.props.groups && this.props.groups.length && this.props.groups.length !== 0 ?
              this.props.groups.map((item) =>
                <div className='group-card' style={{width: this.state.groupCardWidth}} key={item.groupInfo.id} onClick={localStorage.getItem('token') ? () => location.assign(`/group/${item.groupInfo.id}`) : this.showLoginForm}>
                  <UnassembledGroupCard {...item}/>
                </div>
              )
              : <div>Нет результатов</div>
            }
          </Row>
        </Col>
        <SigningInForm visible={this.state.signInVisible} handleCancel={this.handleCancel}/>
      </Row>
    );
  }
}

GroupsPage.defaultProps = {
  groups: []
};

GroupsPage.propTypes = {

};

const mapStateToProps = createStructuredSelector({
  groups: makeSelectGroups()
});

function mapDispatchToProps(dispatch) {
  return {

  };
}

const withConnect = connect(mapStateToProps, mapDispatchToProps);

const withReducer = injectReducer({ key: 'groupsPage', reducer });
const withSaga = injectSaga({ key: 'groupsPage', saga });

export default compose(
  withReducer,
  withSaga,
  withConnect,
)(GroupsPage);
