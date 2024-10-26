using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeviceAPI.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDeviceCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var storedProcedure = @"    
                CREATE OR REPLACE FUNCTION delete_device(deviceId UUID)
                RETURNS void AS $$
                DECLARE
                    conn_string TEXT;
                BEGIN
                    DELETE FROM DMS.public.Devices WHERE Id = device_id;

                    conn_string := 'dbname=UMS user=postgres password=rox123 host=localhost port=5432';

                    PERFORM dblink_exec(
                        conn_string, 
                        'DELETE FROM UMS.public.Devices WHERE Id = ''' || deviceId || ''''
                    );
                END;
                $$ LANGUAGE plpgsql;
            ";
            migrationBuilder.Sql(storedProcedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
