import { createSelector } from 'reselect';

/**
 * Direct selector to the groupsPage state domain
 */
const selectGroupsPageDomain = (state) => state.get('groupsPage');

/**
 * Other specific selectors
 */

const makeSelectGroups = () => createSelector(
  selectGroupsPageDomain,
  (groupsState) => groupsState.get('groups')
);
/**
 * Default selector used by GroupsPage
 */


export {
  selectGroupsPageDomain,
  makeSelectGroups,
};
