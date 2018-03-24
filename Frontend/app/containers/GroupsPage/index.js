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
import {Link} from "react-router-dom";
import { getGroups } from "./actions";
import { makeSelectGroups } from "./selectors";
import {Row, Col, Menu, Dropdown, Icon} from 'antd';
import UnassembledGroupCard from "../../components/UnassembledGroupCard/index";
import AssembledGroupCard from "../../components/AssembledGroupCard/index";

export class GroupsPage extends React.Component { // eslint-disable-line react/prefer-stateless-function
  constructor(props) {
    super(props);

    this.state = {
      title: 'Группы',
      sortTitle: 'Сортировка',
      arrow: 'down'
    }
  }


  sortMenu = (
    <Menu>
      <Menu.Item key="0">
        <div onClick={() => this.setState({sortTitle: 'По возрастанию цены', arrow: 'up'})}>По возрастанию цены</div>
      </Menu.Item>
      <Menu.Item key="1">
        <div onClick={() => this.setState({sortTitle: 'По убыванию цены', arrow: 'down'})}>По убыванию цены</div>
      </Menu.Item>
    </Menu>
  );

  componentDidMount() {
    if(localStorage.getItem('without_server') !== 'true') {
      this.props.getGroups(this.props.match.params.groupsTitle);
      if(this.props.match.params.groupsTitle === 'unassembledGroups') {
        this.setState({title: 'Идет набор'});
      }
      else if(this.props.match.params.groupsTitle === 'assembledGroups') {
          this.setState({title: 'Набранные группы'});
      }
    }
  }

  render() {
    return (
      <Col span={20} offset={2} style={{marginTop: 40}}>
        <Row type='flex' justify='space-between' align='middle'>
          <Col xs={{span: 24}} sm={{span: 12}} style={{fontSize: 24}}>{this.state.title}</Col>

        </Row>
        <Row className='cards-holder cards-holder-center font-size-20' style={{marginTop: 60, marginBottom: 160}}>
          {this.props.match.params.groupsTitle === 'unassembledGroups' ?
            this.props.unassembledGroups.map((item) =>
              <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                <UnassembledGroupCard {...item}/>
              </Link>
            )
            :
            this.props.match.params.groupsTitle === 'assembledGroups' ?
              this.props.assembledGroups.map((item) =>
                <Link key={item.groupInfo.id} to={`/group/${item.groupInfo.id}`}>
                  <AssembledGroupCard {...item}/>
                </Link>
              ) : null
          }
        </Row>
      </Col>
    );
  }
}

GroupsPage.defaultProps = {
  title: 'Группы',
  unassembledGroups: [],
  assembledGroups: []
};

GroupsPage.propTypes = {
  dispatch: PropTypes.func,
};

const mapStateToProps = createStructuredSelector({
  unassembledGroups: makeSelectGroups('unassembledGroups'),
  assembledGroups: makeSelectGroups('assembledGroups')
});

function mapDispatchToProps(dispatch) {
  return {
    getGroups: (typeOfGroups) => dispatch(getGroups(typeOfGroups)),
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
