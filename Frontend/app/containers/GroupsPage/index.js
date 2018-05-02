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
import { getQueryVariable } from "../../globalJS";
import {Link} from "react-router-dom";
import { makeSelectGroups } from "./selectors";
import {Row, Col, Menu, Dropdown, Icon, Card} from 'antd';
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";
import FilterForm from '../../components/GroupsFilterForm';

export class GroupsPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      formed: getQueryVariable('formed') === 'true',
      title: getQueryVariable('name'),
      tags: getQueryVariable('tags')
    };

    this.showFilterForm = this.showFilterForm.bind(this);
  }

  componentDidMount() {

  }

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
        <FilterForm id='lg-filter' title={this.state.title} queryTags={this.state.tags} formed={this.state.formed}/>
        <Col xs={{span: 20, offset: 2}} sm={{span: 16, offset: 4}} lg={{span: 9, offset: 1}} xl={{span: 10, offset: 1}} xxl={{span: 11, offset: 1}} className='groups-content'>
          <Row type='flex' justify='space-between' align='middle'>
            <h3 style={{marginBottom: 0}}>Группы</h3>
          </Row>
          <Row className='cards-holder font-size-20' style={{marginTop: 28}}>
            {this.props.groups && this.props.groups.length && this.props.groups.length !== 0 ?
              this.props.groups.map((item) =>
                <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                  <UnassembledGroupCard {...item}/>
                </Link>
              )
              : <div>Нет результатов</div>
            }
          </Row>
        </Col>
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
