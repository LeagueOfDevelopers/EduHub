import { createSelector } from 'reselect';

/**
 * Direct selector to the HomePage state domain
 */
const selectHomePageDomain = (state) => state.get('homePage');

/**
 * Other specific selectors
 */

const makeSelectUnassembledGroups = () => createSelector(
  makeSelectHomePage,
  (homeState) => homeState.get('unassembledGroups')
)

const makeSelectAssembledGroups = () => createSelector(
  makeSelectHomePage,
  (homeState) => homeState.get('assembledGroups')
)

/**
 * Default selector used by HomePage
 */

const makeSelectHomePage = () => createSelector(
  selectHomePageDomain,
  (substate) => substate.toJS()
);

export {
  selectHomePageDomain,
  makeSelectUnassembledGroups,
  makeSelectAssembledGroups,
  makeSelectHomePage
};