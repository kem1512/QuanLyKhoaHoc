"use client";

import { Grid, NumberInput, Select, TextInput } from "@mantine/core";
import DashboardLayout from "../../components/Layout/DashboardLayout/DashboardLayout";
import {
  BillClient,
  BillCreate,
  BillStatus,
  BillUpdate,
  IBillCreate,
  IBillMapping,
  IBillSingle,
} from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";
import { BillStatusSelect, UserSelect } from "../Helper/AppSelect";

export default function BillHandler({ id }: { id?: number }) {
  const BillService = new BillClient();

  const { data, mutate } = useSWR(
    `/api/bill/${id}`,
    () => BillService.getEntity(id),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const [bill, setBill] = useState<IBillSingle>({
    courseId: 0,
    userId: 0,
    price: 0,
    billStatusId: 0,
  });

  useEffect(() => {
    if (data) setBill(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <UserSelect
            value={bill.user}
            onChange={(e) => setBill((prev) => ({ ...prev, userId: e.id }))}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <BillStatusSelect
            value={bill.billStatus}
            onChange={(e) =>
              setBill((prev) => ({
                ...prev,
                billStatusId: e.id,
                billStatus: e,
              }))
            }
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <NumberInput
            label="Giá"
            placeholder="Nhập Giá"
            value={bill.price}
            onChange={(e: number) => setBill((prev) => ({ ...prev, price: e }))}
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
                    ? BillService.updateEntity(bill.id, bill as BillUpdate)
                    : BillService.createEntity(bill as BillCreate);
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
