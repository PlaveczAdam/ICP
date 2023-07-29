function importAll(r) {
  return Object.fromEntries(r.keys().map((x)=>[x.slice(2),r(x)]));
}

export const itemImages = importAll(
  require.context("../img/ItemIcon", false, /.(png|jpe?g|svg)$/)
);
