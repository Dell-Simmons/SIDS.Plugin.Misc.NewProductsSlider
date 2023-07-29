using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using SIDS.Plugin.Misc.NewProductsSlider.Domains;

namespace SIDS.Plugin.Misc.NewProductsSlider.Mapping.Builders
{
    public class PluginBuilder : NopEntityBuilder<CustomTable>
    {
        #region Methods

        public override void MapEntity(CreateTableExpressionBuilder table)
        {
        }

        #endregion
    }
}