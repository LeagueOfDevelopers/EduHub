import { createSelector } from 'reselect';

/**
 * Direct selector to the groupsPage state domain
 */
const selectGroupsPageDomain = (state) => state.get('groupsPage');

/**
 * Other specific selectors
 */


/**
 * Default selector used by GroupsPage
 */
const makeSelectUnassembledGroups = () => createSelector(
  selectGroupsPageDomain,
  (groupsState) => groupsState.get('unassembledGroups')
);

const makeSelectAssembledGroups = () => createSelector(
  selectGroupsPageDomain,
  (groupsState) => groupsState.get('assembledGroups')
);

export {
  selectGroupsPageDomain,
  makeSelectUnassembledGroups,
  makeSelectAssembledGroups
};
