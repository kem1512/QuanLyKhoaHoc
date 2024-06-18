"use client";

import { Grid, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  IBillStatusMapping,
  BillStatusClient,
  BillStatusCreate,
  BillStatusUpdate,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";

export default function BillStatusHandler({ id }: { id?: number }) {
  const BillStatusService = new BillStatusClient();

  const { data, mutate } = useSWR(`/api/billStatus/${id}`, () =>
    BillStatusService.getEntity(id)
  );

  const [billStatus, setBillStatus] = useState<IBillStatusMapping>({
    name: "",
  });

  useEffect(() => {
    if (data) setBillStatus(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tên Trạng Thái"
            placeholder="Nhập Tên Trạng Thái"
            value={billStatus.name}
            onChange={(e) =>
              setBillStatus((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(
                () => {
                  return id
                    ? BillStatusService.updateEntity(
                        billStatus.id,
                        billStatus as BillStatusUpdate
                      )
                    : BillStatusService.createEntity(
                        billStatus as BillStatusCreate
                      );
                },
                `${id ? "Sửa" : "Thêm"} Thành Công`,
                mutate
              )
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
